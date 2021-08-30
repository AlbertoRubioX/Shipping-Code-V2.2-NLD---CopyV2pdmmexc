namespace WindowsFormsApplication1
{
    partial class M1_Labeling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M1_Labeling));
            this.label1 = new System.Windows.Forms.Label();
            this.txtlote = new System.Windows.Forms.TextBox();
            this.btnok = new System.Windows.Forms.Button();
            this.lblinfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btncerrar = new System.Windows.Forms.Button();
            this.lote_lbl = new System.Windows.Forms.Label();
            this.lblcmp = new System.Windows.Forms.Label();
            this.lbllote = new System.Windows.Forms.Label();
            this.txtciclo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtlocalizacion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txttarima1 = new System.Windows.Forms.TextBox();
            this.txttarima2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(92, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lote :";
            // 
            // txtlote
            // 
            this.txtlote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtlote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlote.ForeColor = System.Drawing.Color.Black;
            this.txtlote.Location = new System.Drawing.Point(147, 54);
            this.txtlote.MaxLength = 9;
            this.txtlote.Name = "txtlote";
            this.txtlote.Size = new System.Drawing.Size(127, 24);
            this.txtlote.TabIndex = 1;
            this.txtlote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtlote_KeyPress);
            this.txtlote.Leave += new System.EventHandler(this.txtlote_Leave);
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.Color.SteelBlue;
            this.btnok.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(280, 55);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(45, 24);
            this.btnok.TabIndex = 0;
            this.btnok.Text = "Ok";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Visible = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // lblinfo
            // 
            this.lblinfo.BackColor = System.Drawing.Color.Transparent;
            this.lblinfo.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinfo.ForeColor = System.Drawing.Color.Black;
            this.lblinfo.Location = new System.Drawing.Point(146, 125);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(231, 72);
            this.lblinfo.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btncerrar);
            this.groupBox1.Controls.Add(this.lote_lbl);
            this.groupBox1.Controls.Add(this.lblcmp);
            this.groupBox1.Controls.Add(this.lbllote);
            this.groupBox1.Controls.Add(this.txtciclo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtlote);
            this.groupBox1.Controls.Add(this.txtlocalizacion);
            this.groupBox1.Controls.Add(this.lblinfo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txttarima1);
            this.groupBox1.Controls.Add(this.btnok);
            this.groupBox1.Controls.Add(this.txttarima2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(64, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 377);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // btncerrar
            // 
            this.btncerrar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btncerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncerrar.ForeColor = System.Drawing.SystemColors.Window;
            this.btncerrar.Image = ((System.Drawing.Image)(resources.GetObject("btncerrar.Image")));
            this.btncerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btncerrar.Location = new System.Drawing.Point(286, 326);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Size = new System.Drawing.Size(122, 44);
            this.btncerrar.TabIndex = 16;
            this.btncerrar.Text = "Cerrar";
            this.btncerrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncerrar.UseVisualStyleBackColor = false;
            this.btncerrar.Click += new System.EventHandler(this.btncerrar_Click);
            // 
            // lote_lbl
            // 
            this.lote_lbl.AutoSize = true;
            this.lote_lbl.BackColor = System.Drawing.Color.Transparent;
            this.lote_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lote_lbl.ForeColor = System.Drawing.Color.Black;
            this.lote_lbl.Location = new System.Drawing.Point(144, 98);
            this.lote_lbl.Name = "lote_lbl";
            this.lote_lbl.Size = new System.Drawing.Size(51, 18);
            this.lote_lbl.TabIndex = 15;
            this.lote_lbl.Text = "Lote :";
            this.lote_lbl.Visible = false;
            // 
            // lblcmp
            // 
            this.lblcmp.AutoSize = true;
            this.lblcmp.ForeColor = System.Drawing.Color.Lime;
            this.lblcmp.Location = new System.Drawing.Point(90, 100);
            this.lblcmp.Name = "lblcmp";
            this.lblcmp.Size = new System.Drawing.Size(40, 16);
            this.lblcmp.TabIndex = 9;
            this.lblcmp.Text = "CMP";
            this.lblcmp.Visible = false;
            // 
            // lbllote
            // 
            this.lbllote.AutoSize = true;
            this.lbllote.BackColor = System.Drawing.Color.Transparent;
            this.lbllote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllote.ForeColor = System.Drawing.Color.Black;
            this.lbllote.Location = new System.Drawing.Point(195, 98);
            this.lbllote.Name = "lbllote";
            this.lbllote.Size = new System.Drawing.Size(51, 18);
            this.lbllote.TabIndex = 14;
            this.lbllote.Text = "Lote :";
            this.lbllote.Visible = false;
            // 
            // txtciclo
            // 
            this.txtciclo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtciclo.Enabled = false;
            this.txtciclo.Location = new System.Drawing.Point(147, 253);
            this.txtciclo.Name = "txtciclo";
            this.txtciclo.Size = new System.Drawing.Size(99, 22);
            this.txtciclo.TabIndex = 4;
            this.txtciclo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtciclo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(90, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ciclo :";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.RoyalBlue;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(286, 253);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 51);
            this.button2.TabIndex = 6;
            this.button2.Text = "Capturar / Imprimir";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtlocalizacion
            // 
            this.txtlocalizacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtlocalizacion.Location = new System.Drawing.Point(146, 304);
            this.txtlocalizacion.Name = "txtlocalizacion";
            this.txtlocalizacion.Size = new System.Drawing.Size(100, 22);
            this.txtlocalizacion.TabIndex = 5;
            this.txtlocalizacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtlocalizacion_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(38, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Localizacion :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(181, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "de";
            // 
            // txttarima1
            // 
            this.txttarima1.Location = new System.Drawing.Point(147, 209);
            this.txttarima1.MaxLength = 3;
            this.txttarima1.Name = "txttarima1";
            this.txttarima1.Size = new System.Drawing.Size(32, 22);
            this.txttarima1.TabIndex = 2;
            this.txttarima1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttarima1_KeyPress);
            // 
            // txttarima2
            // 
            this.txttarima2.Location = new System.Drawing.Point(211, 209);
            this.txttarima2.MaxLength = 3;
            this.txttarima2.Name = "txttarima2";
            this.txttarima2.Size = new System.Drawing.Size(35, 22);
            this.txttarima2.TabIndex = 3;
            this.txttarima2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttarima2_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(76, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tarimas:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(534, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(127, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(61, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Impresión";
            // 
            // M1_Labeling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.back_screen1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(534, 474);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "M1_Labeling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Etiquetado/Captura";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtlote;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.Label lblinfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txttarima1;
        private System.Windows.Forms.TextBox txttarima2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtlocalizacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtciclo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lbllote;
        private System.Windows.Forms.Label lblcmp;
        private System.Windows.Forms.Label lote_lbl;
        private System.Windows.Forms.Button btncerrar;
        private System.Windows.Forms.Label label6;
    }
}

