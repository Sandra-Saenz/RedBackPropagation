namespace UI
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtIteracion = new System.Windows.Forms.TextBox();
            this.TxtErrorMax = new System.Windows.Forms.TextBox();
            this.TxtRata = new System.Windows.Forms.TextBox();
            this.TxtArchivo = new System.Windows.Forms.TextBox();
            this.BtnLimpiar = new System.Windows.Forms.Button();
            this.BtnExaminar = new System.Windows.Forms.Button();
            this.BtnEntrenar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtPatron = new System.Windows.Forms.TextBox();
            this.BtnSimular = new System.Windows.Forms.Button();
            this.LbTablaSimulacion = new System.Windows.Forms.ListBox();
            this.lbTablaProblema = new System.Windows.Forms.ListBox();
            this.lbTablaSolucion = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(199, 538);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(418, 188);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(309, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "RED BACKPROPAGATION";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Iteraciones";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Error maximo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Rata";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Archivo";
            // 
            // TxtIteracion
            // 
            this.TxtIteracion.Location = new System.Drawing.Point(137, 77);
            this.TxtIteracion.Name = "TxtIteracion";
            this.TxtIteracion.Size = new System.Drawing.Size(100, 20);
            this.TxtIteracion.TabIndex = 6;
            // 
            // TxtErrorMax
            // 
            this.TxtErrorMax.Location = new System.Drawing.Point(137, 115);
            this.TxtErrorMax.Name = "TxtErrorMax";
            this.TxtErrorMax.Size = new System.Drawing.Size(100, 20);
            this.TxtErrorMax.TabIndex = 7;
            // 
            // TxtRata
            // 
            this.TxtRata.Location = new System.Drawing.Point(137, 146);
            this.TxtRata.Name = "TxtRata";
            this.TxtRata.Size = new System.Drawing.Size(100, 20);
            this.TxtRata.TabIndex = 8;
            // 
            // TxtArchivo
            // 
            this.TxtArchivo.Location = new System.Drawing.Point(137, 183);
            this.TxtArchivo.Name = "TxtArchivo";
            this.TxtArchivo.Size = new System.Drawing.Size(201, 20);
            this.TxtArchivo.TabIndex = 9;
            // 
            // BtnLimpiar
            // 
            this.BtnLimpiar.Location = new System.Drawing.Point(64, 247);
            this.BtnLimpiar.Name = "BtnLimpiar";
            this.BtnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.BtnLimpiar.TabIndex = 10;
            this.BtnLimpiar.Text = "Limpiar";
            this.BtnLimpiar.UseVisualStyleBackColor = true;
            this.BtnLimpiar.Click += new System.EventHandler(this.BtnLimpiar_Click);
            // 
            // BtnExaminar
            // 
            this.BtnExaminar.Location = new System.Drawing.Point(159, 246);
            this.BtnExaminar.Name = "BtnExaminar";
            this.BtnExaminar.Size = new System.Drawing.Size(75, 23);
            this.BtnExaminar.TabIndex = 11;
            this.BtnExaminar.Text = "Examinar";
            this.BtnExaminar.UseVisualStyleBackColor = true;
            this.BtnExaminar.Click += new System.EventHandler(this.BtnExaminar_Click);
            // 
            // BtnEntrenar
            // 
            this.BtnEntrenar.Location = new System.Drawing.Point(263, 247);
            this.BtnEntrenar.Name = "BtnEntrenar";
            this.BtnEntrenar.Size = new System.Drawing.Size(75, 23);
            this.BtnEntrenar.TabIndex = 12;
            this.BtnEntrenar.Text = "Entrenar";
            this.BtnEntrenar.UseVisualStyleBackColor = true;
            this.BtnEntrenar.Click += new System.EventHandler(this.BtnEntrenar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 299);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Patron";
            // 
            // TxtPatron
            // 
            this.TxtPatron.Location = new System.Drawing.Point(137, 294);
            this.TxtPatron.Name = "TxtPatron";
            this.TxtPatron.Size = new System.Drawing.Size(100, 20);
            this.TxtPatron.TabIndex = 14;
            // 
            // BtnSimular
            // 
            this.BtnSimular.Location = new System.Drawing.Point(263, 294);
            this.BtnSimular.Name = "BtnSimular";
            this.BtnSimular.Size = new System.Drawing.Size(75, 23);
            this.BtnSimular.TabIndex = 15;
            this.BtnSimular.Text = "Simular";
            this.BtnSimular.UseVisualStyleBackColor = true;
            this.BtnSimular.Click += new System.EventHandler(this.BtnSimular_Click);
            // 
            // LbTablaSimulacion
            // 
            this.LbTablaSimulacion.FormattingEnabled = true;
            this.LbTablaSimulacion.Location = new System.Drawing.Point(61, 359);
            this.LbTablaSimulacion.Name = "LbTablaSimulacion";
            this.LbTablaSimulacion.Size = new System.Drawing.Size(318, 121);
            this.LbTablaSimulacion.TabIndex = 16;
            // 
            // lbTablaProblema
            // 
            this.lbTablaProblema.FormattingEnabled = true;
            this.lbTablaProblema.Location = new System.Drawing.Point(439, 70);
            this.lbTablaProblema.Name = "lbTablaProblema";
            this.lbTablaProblema.Size = new System.Drawing.Size(332, 199);
            this.lbTablaProblema.TabIndex = 17;
            // 
            // lbTablaSolucion
            // 
            this.lbTablaSolucion.FormattingEnabled = true;
            this.lbTablaSolucion.Location = new System.Drawing.Point(439, 281);
            this.lbTablaSolucion.Name = "lbTablaSolucion";
            this.lbTablaSolucion.Size = new System.Drawing.Size(332, 199);
            this.lbTablaSolucion.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 749);
            this.Controls.Add(this.lbTablaSolucion);
            this.Controls.Add(this.lbTablaProblema);
            this.Controls.Add(this.LbTablaSimulacion);
            this.Controls.Add(this.BtnSimular);
            this.Controls.Add(this.TxtPatron);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BtnEntrenar);
            this.Controls.Add(this.BtnExaminar);
            this.Controls.Add(this.BtnLimpiar);
            this.Controls.Add(this.TxtArchivo);
            this.Controls.Add(this.TxtRata);
            this.Controls.Add(this.TxtErrorMax);
            this.Controls.Add(this.TxtIteracion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtIteracion;
        private System.Windows.Forms.TextBox TxtErrorMax;
        private System.Windows.Forms.TextBox TxtRata;
        private System.Windows.Forms.TextBox TxtArchivo;
        private System.Windows.Forms.Button BtnLimpiar;
        private System.Windows.Forms.Button BtnExaminar;
        private System.Windows.Forms.Button BtnEntrenar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtPatron;
        private System.Windows.Forms.Button BtnSimular;
        private System.Windows.Forms.ListBox LbTablaSimulacion;
        private System.Windows.Forms.ListBox lbTablaProblema;
        private System.Windows.Forms.ListBox lbTablaSolucion;
    }
}

