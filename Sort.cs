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
        List<List<double>> RunStack=new List<List<double>>();//Quase uma pilha de todos os subarrays de Run...

        public Sort(double[] mass)
        {
            this.mass = mass;
            this.minrun = CalculMinRun();//Calcule minrun imediatamente...
        }

        /// <summary>
        /// método de cálculo minrun...
        /// </summary>
        /// <returns>O tamanho mínimo de uma matriz ao dividir...</returns>
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
        /// Este método implementa a lógica de criação de um array Run,
        /// que é, em última análise, uma sequência e, na melhor das hipóteses, um intervalo...
        /// </summary>
        /// <param name="start_index">O número do elemento da matriz a partir do qual começamos a compor
        /// executar matriz</param>
        /// <returns>Retorna uma matriz Run que é estritamente crescente,
        /// ou não descendente...
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
        /// Método auxiliar para definir um array estritamente decrescente...
        /// </summary>
        /// <param name="Run">Matriz verificada Executar...</param>
        /// <returns>True se o array estiver estritamente decrescente, False caso contrário...</returns>
        private bool TestWaningRun(List<double> Run)
        {
            for (int i = 0; i < Run.Count-1; i++)
            {
                if (Run[i] >= Run[i + 1]) return false;
            }
            return true;
        }

        /// <summary>
        /// Continuação lógica do CalculRunMass...
        /// Converte um array estritamente decrescente em um array estritamente crescente...
        /// </summary>
        /// <param name="Run">Array Run (estritamente decrescente)...</param>
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
        /// Ordenação por inserção clássica... Já que a entrada será
        /// array quase ordenado, a ordenação será muito rápida...
        /// </summary>
        /// <param name="Run">Executar array classificável...</param>
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
        /// Procedimento de montagem...Inclui toda a lógica para dividir um array em subarrays Executar...
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
                    //Digamos que isso funcione...
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
        /// Procedure-layout... A fase final da nossa ordenação... Implementa toda a lógica
        /// associado ao método MergeSort, em particular resolve o problema de fornecer um array menor
        /// mais à esquerda do que mais...
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
        /// Um método público que implementa toda a sequência de ações do algoritmo TimSort...
        /// </summary>
        /// <returns>Retorna array ordenado...</returns>
        public List<double> Sorting()
        {
            SecondStep();
            ThridStep();
            return RunStack[0];
        }

        /// <summary>
        /// O algoritmo clássico para mesclar dois arrays em um...
        /// P.S O algoritmo TimSort implica uma modificação deste método, nomeadamente
        /// Modificação do modo de galope...
        /// </summary>
        /// <param name="left">Um array com um tamanho menor...</param>
        /// <param name="right">Grande matriz...</param>
        /// <returns>Retorna o array concatenado...</returns>
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
