using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp10
{
    class Sort
    {
        double[] mass;
        int minrun;
        List<List<double>> RunStack=new List<List<double>>();//Почти стэк всех подмассивов Run...

        public Sort(double[] mass)
        {
            this.mass = mass;
            this.minrun = CalculMinRun();//Сразу вычисляем minrun...
        }

        /// <summary>
        /// Метод расчета minrun...
        /// </summary>
        /// <returns>Минимальный размер массива при разбиении...</returns>
        private int CalculMinRun()
        {
            int r = 0;
            int n = mass.Length;
            while (n >= 64)
            {
                r |= n & 1;
                n >>= 1;
            }
            return n + r;
        }

        /// <summary>
        /// В данном методе реализована логика составления Run-массива,
        /// который в конечном является последовательностью,а в лучшем случае диапозоном...
        /// </summary>
        /// <param name="start_index">Номер элемента массива с которого начинаем составлять
        /// массив Run</param>
        /// <returns>Возвращает Run-массив,который либо строго упорядочен по-возрастанию,
        /// либо по не-убыванию...
        /// </returns>
        private List<double> CalculRunMass(int start_index)
        {
            List<double> Run = new List<double>();
            Run.Add(mass[start_index]);
            Run.Add(mass[start_index + 1]);
            bool type_mass;
            if (Run[0] > Run[1]) type_mass = true; else type_mass = false;  
            for (int j = start_index+2; j < mass.Length; j++)
            {
                if (type_mass)
                {
                    if (Run.Last() > mass[j]) Run.Add(mass[j]); else return Run;
                }
                else
                {
                    if (Run.Last() <= mass[j]) Run.Add(mass[j]); else return Run;
                }
            }
            return Run;
        }

        /// <summary>
        /// Вспомогательный метод для определения строго убывыющего массива...
        /// </summary>
        /// <param name="Run">Проверяемый массив Run...</param>
        /// <returns>True-если массив строго убывающий,False-в противном случае...</returns>
        private bool TestWaningRun(List<double> Run)
        {
            for (int i = 0; i < Run.Count-1; i++)
            {
                if (Run[i] >= Run[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Логическое продолжение метода CalculRunMass...
        /// Переделывает строго убывающий массив в строго возрастающий...
        /// </summary>
        /// <param name="Run">Массив Run(строго убывающий)...</param>
        private void ChangeLocation(ref List<double> Run)
        {
            for (int i = 0; i < Run.Count; i++)
            {
                double left = Run[i];
                double right = Run[Run.Count - (i + 1)];
                if (i != (Run.Count - (i + 1)))
                {
                    Run[i] = right;
                    Run[Run.Count - (i + 1)] = left;
                }
            }
        }

        /// <summary>
        /// Классическая сортировка вставками...Поскольку на вход будет подаваться
        /// почти отсортированный массив,сортировка будет очень быстрой...
        /// </summary>
        /// <param name="Run">Run-сортируемый массив...</param>
        private void InsertionSort(ref List<double> Run)
        {
            for (int i = 0; i < Run.Count; i++)
            {
                double temp = Run[i];
                int j = i - 1;
                while (j >= 0 && Run[j] > temp)
                {
                    Run[j + 1] = Run[j];
                    j--;
                }
                Run[j + 1] = temp;
            }
        }

        /// <summary>
        /// Процедура-компоновка...Включает всю логику по разделению массива,на подмассивы Run...
        /// </summary>
        private void SecondStep()
        {
            for (int i = 0; i < mass.Length; )
            {
               List<double> current_Run= CalculRunMass(i);
               if (TestWaningRun(current_Run)) ChangeLocation(ref current_Run); 
               if(current_Run.Count<this.minrun)
               {
                    int lenght_current_run = current_Run.Count;
                    int right = lenght_current_run + (minrun - lenght_current_run);
                    //Допустим,это будет работать...
                    for (int k = lenght_current_run; k < right; k++)
                    {
                        if (i + k < mass.Length) current_Run.Add(mass[i + k]);
                        else
                            break;
                    }
               }
               InsertionSort(ref current_Run);
                RunStack.Add(new List<double>(current_Run));
                i += current_Run.Count;
            }
        }

        /// <summary>
        /// Процедура-компоновка...Конечный этам нашей сортировки...Реализует всю логику 
        /// связанную с методом MergeSort,в частности решает проблему подачи меньшего массива
        /// левее,чем большего...
        /// </summary>
        private void ThridStep()
        {
            RunStack.Reverse();
            for (int i = 0; i < RunStack.Count-2;)
            {
                List<double> merge_mass = new List<double>();
                if(RunStack[i].Count>RunStack[i+1].Count+RunStack[i+2].Count&&
                     RunStack[i + 1].Count >RunStack[i + 2].Count)
                {
                    if (RunStack[i].Count < RunStack[i + 1].Count) merge_mass= MergeMass(RunStack[i], RunStack[i + 1]);
                    else
                       merge_mass= MergeMass(RunStack[i + 1], RunStack[i]);
                    RunStack[i] = merge_mass;
                    RunStack.RemoveAt(i + 1);
                }
                else
                {
                    if (RunStack[i].Count <= RunStack[i + 2].Count)
                    {
                        if (RunStack[i].Count < RunStack[i + 1].Count) merge_mass = MergeMass(RunStack[i], RunStack[i + 1]);
                        else merge_mass= MergeMass(RunStack[i + 1], RunStack[i]);
                        RunStack[i] = merge_mass;
                        RunStack.RemoveAt(i + 1);
                    }
                    else
                    {
                        if (RunStack[i+2].Count < RunStack[i + 1].Count) merge_mass = MergeMass(RunStack[i+2], RunStack[i + 1]);
                        else merge_mass = MergeMass(RunStack[i + 1], RunStack[i+2]);
                        RunStack[i+1] = merge_mass;
                        RunStack.RemoveAt(i + 2);
                    }
                }
            }

            if (RunStack.Count == 2)
            {
                List<double> merge_mass=new List<double>();
                if (RunStack[0].Count < RunStack[1].Count) merge_mass = MergeMass(RunStack[0], RunStack[1]);
                else merge_mass = MergeMass(RunStack[1], RunStack[0]);
                RunStack.Clear();
                RunStack.Add(merge_mass);
            }
        }

        /// <summary>
        /// Публичный метод реализующий всю последовательность действий алгоритма TimSort...
        /// </summary>
        /// <returns>Возвращает отсортированный массив...</returns>
        public List<double> Sorting()
        {
            SecondStep();
            ThridStep();
            return RunStack[0];
        }

        /// <summary>
        /// Классический алгоритм слияния двух массивов в один...
        /// P.S Алгоритм TimSort подразумевает модификацию данного метода,а именно
        /// модификацию Galloping mode...
        /// </summary>
        /// <param name="left">Массив с меньшим размером...</param>
        /// <param name="right">Массив с большим размером...</param>
        /// <returns>Возвращает объединенный массив...</returns>
        private List<double> MergeMass(List<double> left,List<double> right)
        {
            List<double> output = new List<double>();
            while (left.Count != 0 && right.Count != 0)
            {
                if (left[0] >= right[0])
                {
                    output.Add(right[0]);
                    right.RemoveAt(0);
                }
                else
                {
                    output.Add(left[0]);
                    left.RemoveAt(0);
                }
            }
            if (left.Count != 0)
            {
                for (int i = 0; i < left.Count; i++)
                {
                    output.Add(left[i]);
                }
            }
            if (right.Count != 0)
            {
                for (int i = 0; i < right.Count; i++)
                {
                    output.Add(right[i]);
                }
            }
            return output;
        }
    }
}
