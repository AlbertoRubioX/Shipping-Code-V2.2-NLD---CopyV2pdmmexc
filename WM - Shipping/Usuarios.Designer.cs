namespace WindowsFormsApplication1
{
    partial class Usuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usuarios));
            this.alta = new System.Windows.Forms.RadioButton();
            this.Modificacion = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gpalta = new System.Windows.Forms.GroupBox();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.btn_ok_alta = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbacceso = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtcontra2 = new System.Windows.Forms.TextBox();
            this.txtcontra1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtempleado = new System.Windows.Forms.TextBox();
            this.gbbaja = new System.Windows.Forms.GroupBox();
            this.btn_ok_baja = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtempleado2 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.gpalta.SuspendLayout();
            this.gbbaja.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // alta
            // 
            this.alta.AutoSize = true;
            this.alta.Location = new System.Drawing.Point(35, 42);
            this.alta.Margin = new System.Windows.Forms.Padding(4);
            this.alta.Name = "alta";
            this.alta.Size = new System.Drawing.Size(57, 21);
            this.alta.TabIndex = 0;
            this.alta.TabStop = true;
            this.alta.Text = "Alta";
            this.alta.UseVisualStyleBackColor = true;
            this.alta.CheckedChanged += new System.EventHandler(this.alta_CheckedChanged);
            // 
            // Modificacion
            // 
            this.Modificacion.AutoSize = true;
            this.Modificacion.Location = new System.Drawing.Point(35, 71);
            this.Modificacion.Margin = new System.Windows.Forms.Padding(4);
            this.Modificacion.Name = "Modificacion";
            this.Modificacion.Size = new System.Drawing.Size(61, 21);
            this.Modificacion.TabIndex = 1;
            this.Modificacion.TabStop = true;
            this.Modificacion.Text = "Baja";
            this.Modificacion.UseVisualStyleBackColor = true;
            this.Modificacion.CheckedChanged += new System.EventHandler(this.Modificacion_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.alta);
            this.groupBox1.Controls.Add(this.Modificacion);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(40, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(177, 164);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "  Opciones  ";
            // 
            // gpalta
            // 
            this.gpalta.BackColor = System.Drawing.Color.Transparent;
            this.gpalta.Controls.Add(this.txtnombre);
            this.gpalta.Controls.Add(this.btn_ok_alta);
            this.gpalta.Controls.Add(this.label5);
            this.gpalta.Controls.Add(this.cbacceso);
            this.gpalta.Controls.Add(this.label4);
            this.gpalta.Controls.Add(this.txtcontra2);
            this.gpalta.Controls.Add(this.txtcontra1);
            this.gpalta.Controls.Add(this.label3);
            this.gpalta.Controls.Add(this.label2);
            this.gpalta.Controls.Add(this.label1);
            this.gpalta.Controls.Add(this.txtempleado);
            this.gpalta.Location = new System.Drawing.Point(225, 26);
            this.gpalta.Margin = new System.Windows.Forms.Padding(4);
            this.gpalta.Name = "gpalta";
            this.gpalta.Padding = new System.Windows.Forms.Padding(4);
            this.gpalta.Size = new System.Drawing.Size(437, 321);
            this.gpalta.TabIndex = 4;
            this.gpalta.TabStop = false;
            // 
            // txtnombre
            // 
            this.txtnombre.Location = new System.Drawing.Point(228, 69);
            this.txtnombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(157, 22);
            this.txtnombre.TabIndex = 1;
            // 
            // btn_ok_alta
            // 
            this.btn_ok_alta.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_ok_alta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ok_alta.ForeColor = System.Drawing.Color.White;
            this.btn_ok_alta.Image = ((System.Drawing.Image)(resources.GetObject("btn_ok_alta.Image")));
            this.btn_ok_alta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ok_alta.Location = new System.Drawing.Point(306, 265);
            this.btn_ok_alta.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ok_alta.Name = "btn_ok_alta";
            this.btn_ok_alta.Size = new System.Drawing.Size(124, 49);
            this.btn_ok_alta.TabIndex = 6;
            this.btn_ok_alta.Text = "Guardar";
            this.btn_ok_alta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_ok_alta.UseVisualStyleBackColor = false;
            this.btn_ok_alta.Click += new System.EventHandler(this.btn_ok_alta_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(135, 71);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nombre :";
            // 
            // cbacceso
            // 
            this.cbacceso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbacceso.FormattingEnabled = true;
            this.cbacceso.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbacceso.Location = new System.Drawing.Point(228, 217);
            this.cbacceso.Margin = new System.Windows.Forms.Padding(4);
            this.cbacceso.Name = "cbacceso";
            this.cbacceso.Size = new System.Drawing.Size(157, 24);
            this.cbacceso.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(67, 220);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nivel de Acceso :";
            // 
            // txtcontra2
            // 
            this.txtcontra2.Location = new System.Drawing.Point(228, 159);
            this.txtcontra2.Margin = new System.Windows.Forms.Padding(4);
            this.txtcontra2.Name = "txtcontra2";
            this.txtcontra2.PasswordChar = '•';
            this.txtcontra2.Size = new System.Drawing.Size(157, 22);
            this.txtcontra2.TabIndex = 3;
            // 
            // txtcontra1
            // 
            this.txtcontra1.Location = new System.Drawing.Point(228, 117);
            this.txtcontra1.Margin = new System.Windows.Forms.Padding(4);
            this.txtcontra1.Name = "txtcontra1";
            this.txtcontra1.PasswordChar = '•';
            this.txtcontra1.Size = new System.Drawing.Size(157, 22);
            this.txtcontra1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(35, 162);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Confirme Contraseña :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(105, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(32, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Numero de Empleado :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtempleado
            // 
            this.txtempleado.Location = new System.Drawing.Point(228, 30);
            this.txtempleado.Margin = new System.Windows.Forms.Padding(4);
            this.txtempleado.Name = "txtempleado";
            this.txtempleado.Size = new System.Drawing.Size(157, 22);
            this.txtempleado.TabIndex = 0;
            // 
            // gbbaja
            // 
            this.gbbaja.BackColor = System.Drawing.Color.Transparent;
            this.gbbaja.Controls.Add(this.btn_ok_baja);
            this.gbbaja.Controls.Add(this.label6);
            this.gbbaja.Controls.Add(this.txtempleado2);
            this.gbbaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbbaja.ForeColor = System.Drawing.Color.White;
            this.gbbaja.Location = new System.Drawing.Point(225, 26);
            this.gbbaja.Margin = new System.Windows.Forms.Padding(4);
            this.gbbaja.Name = "gbbaja";
            this.gbbaja.Padding = new System.Windows.Forms.Padding(4);
            this.gbbaja.Size = new System.Drawing.Size(437, 321);
            this.gbbaja.TabIndex = 6;
            this.gbbaja.TabStop = false;
            // 
            // btn_ok_baja
            // 
            this.btn_ok_baja.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_ok_baja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ok_baja.ForeColor = System.Drawing.Color.White;
            this.btn_ok_baja.Image = ((System.Drawing.Image)(resources.GetObject("btn_ok_baja.Image")));
            this.btn_ok_baja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ok_baja.Location = new System.Drawing.Point(298, 265);
            this.btn_ok_baja.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ok_baja.Name = "btn_ok_baja";
            this.btn_ok_baja.Size = new System.Drawing.Size(132, 49);
            this.btn_ok_baja.TabIndex = 7;
            this.btn_ok_baja.Text = "Guardar";
            this.btn_ok_baja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_ok_baja.UseVisualStyleBackColor = false;
            this.btn_ok_baja.Click += new System.EventHandler(this.btn_ok_baja_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(32, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Numero de Empleado :";
            // 
            // txtempleado2
            // 
            this.txtempleado2.Location = new System.Drawing.Point(228, 64);
            this.txtempleado2.Margin = new System.Windows.Forms.Padding(4);
            this.txtempleado2.Name = "txtempleado2";
            this.txtempleado2.Size = new System.Drawing.Size(157, 24);
            this.txtempleado2.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 381);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(713, 25);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(160, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.back_screen1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(713, 406);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbbaja);
            this.Controls.Add(this.gpalta);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Usuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usuarios";
            this.Load += new System.EventHandler(this.Usuarios_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpalta.ResumeLayout(false);
            this.gpalta.PerformLayout();
            this.gbbaja.ResumeLayout(false);
            this.gbbaja.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton alta;
        private System.Windows.Forms.RadioButton Modificacion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gpalta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtempleado;
        private System.Windows.Forms.ComboBox cbacceso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtcontra2;
        private System.Windows.Forms.TextBox txtcontra1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_ok_alta;
        private System.Windows.Forms.GroupBox gbbaja;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtempleado2;
        private System.Windows.Forms.Button btn_ok_baja;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}