namespace WindowsFormsApp10
{
    partial class Form1
    {
        /// <summary>
        /// Variável construtora obrigatória.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Libere todos os recursos usados.
        /// </summary>
        /// <param name="disposing">true se o recurso gerenciado deve ser excluído; caso contrário, falso.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.sort_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.open_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // sort_button
            // 
            this.sort_button.Location = new System.Drawing.Point(174, 39);
            this.sort_button.Name = "sort_button";
            this.sort_button.Size = new System.Drawing.Size(102, 23);
            this.sort_button.TabIndex = 0;
            this.sort_button.Text = "Ordenação";
            this.sort_button.UseVisualStyleBackColor = true;
            this.sort_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 20);
            this.textBox1.TabIndex = 1;
            // 
            // open_button
            // 
            this.open_button.Location = new System.Drawing.Point(174, 10);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(102, 23);
            this.open_button.TabIndex = 2;
            this.open_button.Text = "Abrir arquivo";
            this.open_button.UseVisualStyleBackColor = true;
            this.open_button.Click += new System.EventHandler(this.open_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 73);
            this.Controls.Add(this.open_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.sort_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sort_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button open_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

