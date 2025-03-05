namespace DataTransform
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExecuta = new System.Windows.Forms.Button();
            this.progressDTS = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgListagem = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIncluidos = new System.Windows.Forms.Label();
            this.lblErros = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListagem)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExecuta
            // 
            this.btnExecuta.Location = new System.Drawing.Point(348, 392);
            this.btnExecuta.Name = "btnExecuta";
            this.btnExecuta.Size = new System.Drawing.Size(75, 23);
            this.btnExecuta.TabIndex = 0;
            this.btnExecuta.Text = "Executar";
            this.btnExecuta.UseVisualStyleBackColor = true;
            this.btnExecuta.Click += new System.EventHandler(this.btnExecuta_Click);
            // 
            // progressDTS
            // 
            this.progressDTS.Location = new System.Drawing.Point(23, 351);
            this.progressDTS.Name = "progressDTS";
            this.progressDTS.Size = new System.Drawing.Size(734, 23);
            this.progressDTS.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.dgListagem);
            this.panel1.Location = new System.Drawing.Point(23, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 281);
            this.panel1.TabIndex = 3;
            // 
            // dgListagem
            // 
            this.dgListagem.AllowUserToAddRows = false;
            this.dgListagem.AllowUserToDeleteRows = false;
            this.dgListagem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgListagem.Location = new System.Drawing.Point(18, 17);
            this.dgListagem.Name = "dgListagem";
            this.dgListagem.ReadOnly = true;
            this.dgListagem.Size = new System.Drawing.Size(688, 251);
            this.dgListagem.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Total de Registros:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Incluídos:";
            // 
            // lblIncluidos
            // 
            this.lblIncluidos.AutoSize = true;
            this.lblIncluidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncluidos.ForeColor = System.Drawing.Color.Green;
            this.lblIncluidos.Location = new System.Drawing.Point(369, 312);
            this.lblIncluidos.Name = "lblIncluidos";
            this.lblIncluidos.Size = new System.Drawing.Size(0, 13);
            this.lblIncluidos.TabIndex = 6;
            // 
            // lblErros
            // 
            this.lblErros.AutoSize = true;
            this.lblErros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErros.ForeColor = System.Drawing.Color.Red;
            this.lblErros.Location = new System.Drawing.Point(472, 312);
            this.lblErros.Name = "lblErros";
            this.lblErros.Size = new System.Drawing.Size(0, 13);
            this.lblErros.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(432, 313);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Erros:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 425);
            this.Controls.Add(this.lblErros);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblIncluidos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressDTS);
            this.Controls.Add(this.btnExecuta);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgListagem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecuta;
        private System.Windows.Forms.ProgressBar progressDTS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgListagem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIncluidos;
        private System.Windows.Forms.Label lblErros;
        private System.Windows.Forms.Label label4;
    }
}

