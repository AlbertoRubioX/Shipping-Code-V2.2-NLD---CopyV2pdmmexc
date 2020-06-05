namespace WindowsFormsApplication1
{
    partial class Fedex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fedex));
            this.button2 = new System.Windows.Forms.Button();
            this.btnncarga = new System.Windows.Forms.Button();
            this.dtvgrid = new System.Windows.Forms.DataGridView();
            this.btnagregar = new System.Windows.Forms.Button();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtenvio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtlote = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcantidad = new System.Windows.Forms.TextBox();
            this.txtrestante = new System.Windows.Forms.TextBox();
            this.txtposicion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txttarima1 = new System.Windows.Forms.TextBox();
            this.txttarima2 = new System.Windows.Forms.TextBox();
            this.btnCerrarcarga = new System.Windows.Forms.Button();
            this.Actualizar = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtretorno = new System.Windows.Forms.TextBox();
            this.txtcargado = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtposition = new System.Windows.Forms.TextBox();
            this.txtnp = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtnlote = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtoidcarga = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txttotal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnGuardarAct = new System.Windows.Forms.Button();
            this.txtship = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtbrach = new System.Windows.Forms.TextBox();
            this.txtlotefedex = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtpartnumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtidembarque = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Cargado = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtvgrid)).BeginInit();
            this.Actualizar.SuspendLayout();
            this.Cargado.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.RoyalBlue;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.Window;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(1059, 11);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 54);
            this.button2.TabIndex = 13;
            this.button2.Text = "Cerrar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnncarga
            // 
            this.btnncarga.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnncarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnncarga.ForeColor = System.Drawing.SystemColors.Window;
            this.btnncarga.Image = ((System.Drawing.Image)(resources.GetObject("btnncarga.Image")));
            this.btnncarga.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnncarga.Location = new System.Drawing.Point(519, 13);
            this.btnncarga.Margin = new System.Windows.Forms.Padding(4);
            this.btnncarga.Name = "btnncarga";
            this.btnncarga.Size = new System.Drawing.Size(173, 54);
            this.btnncarga.TabIndex = 14;
            this.btnncarga.Text = "Nueva Carga";
            this.btnncarga.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnncarga.UseVisualStyleBackColor = false;
            this.btnncarga.Click += new System.EventHandler(this.btnncarga_Click);
            // 
            // dtvgrid
            // 
            this.dtvgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtvgrid.Location = new System.Drawing.Point(6, 21);
            this.dtvgrid.Name = "dtvgrid";
            this.dtvgrid.RowTemplate.Height = 24;
            this.dtvgrid.Size = new System.Drawing.Size(576, 292);
            this.dtvgrid.TabIndex = 15;
            // 
            // btnagregar
            // 
            this.btnagregar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnagregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnagregar.ForeColor = System.Drawing.SystemColors.Window;
            this.btnagregar.Image = ((System.Drawing.Image)(resources.GetObject("btnagregar.Image")));
            this.btnagregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnagregar.Location = new System.Drawing.Point(260, 274);
            this.btnagregar.Margin = new System.Windows.Forms.Padding(4);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(129, 54);
            this.btnagregar.TabIndex = 16;
            this.btnagregar.Text = "Agregar";
            this.btnagregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnagregar.UseVisualStyleBackColor = false;
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // btnbuscar
            // 
            this.btnbuscar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnbuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbuscar.ForeColor = System.Drawing.SystemColors.Window;
            this.btnbuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnbuscar.Image")));
            this.btnbuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnbuscar.Location = new System.Drawing.Point(322, 13);
            this.btnbuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(129, 54);
            this.btnbuscar.TabIndex = 17;
            this.btnbuscar.Text = "Buscar";
            this.btnbuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnbuscar.UseVisualStyleBackColor = false;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(173, 29);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(81, 22);
            this.txtId.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(87, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Mapa Id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(104, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Envio:";
            // 
            // txtenvio
            // 
            this.txtenvio.Location = new System.Drawing.Point(173, 74);
            this.txtenvio.Name = "txtenvio";
            this.txtenvio.Size = new System.Drawing.Size(136, 22);
            this.txtenvio.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(113, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 18);
            this.label3.TabIndex = 22;
            this.label3.Text = "Lote:";
            // 
            // txtlote
            // 
            this.txtlote.Location = new System.Drawing.Point(173, 116);
            this.txtlote.Name = "txtlote";
            this.txtlote.Size = new System.Drawing.Size(136, 22);
            this.txtlote.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(80, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 18);
            this.label4.TabIndex = 24;
            this.label4.Text = "Cantidad:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(79, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 25;
            this.label5.Text = "Restante:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(79, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 18);
            this.label6.TabIndex = 26;
            this.label6.Text = "Posicion:";
            // 
            // txtcantidad
            // 
            this.txtcantidad.Location = new System.Drawing.Point(173, 187);
            this.txtcantidad.Name = "txtcantidad";
            this.txtcantidad.Size = new System.Drawing.Size(136, 22);
            this.txtcantidad.TabIndex = 27;
            // 
            // txtrestante
            // 
            this.txtrestante.Location = new System.Drawing.Point(173, 224);
            this.txtrestante.Name = "txtrestante";
            this.txtrestante.Size = new System.Drawing.Size(136, 22);
            this.txtrestante.TabIndex = 28;
            // 
            // txtposicion
            // 
            this.txtposicion.Location = new System.Drawing.Point(173, 266);
            this.txtposicion.Name = "txtposicion";
            this.txtposicion.Size = new System.Drawing.Size(57, 22);
            this.txtposicion.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(79, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "Tarimas:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(228, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 18);
            this.label8.TabIndex = 31;
            this.label8.Text = "de";
            // 
            // txttarima1
            // 
            this.txttarima1.Location = new System.Drawing.Point(173, 152);
            this.txttarima1.Name = "txttarima1";
            this.txttarima1.Size = new System.Drawing.Size(42, 22);
            this.txttarima1.TabIndex = 32;
            this.txttarima1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttarima1_KeyPress);
            // 
            // txttarima2
            // 
            this.txttarima2.Enabled = false;
            this.txttarima2.Location = new System.Drawing.Point(260, 152);
            this.txttarima2.Name = "txttarima2";
            this.txttarima2.Size = new System.Drawing.Size(42, 22);
            this.txttarima2.TabIndex = 33;
            // 
            // btnCerrarcarga
            // 
            this.btnCerrarcarga.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCerrarcarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarcarga.ForeColor = System.Drawing.SystemColors.Window;
            this.btnCerrarcarga.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarcarga.Image")));
            this.btnCerrarcarga.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarcarga.Location = new System.Drawing.Point(413, 274);
            this.btnCerrarcarga.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrarcarga.Name = "btnCerrarcarga";
            this.btnCerrarcarga.Size = new System.Drawing.Size(156, 54);
            this.btnCerrarcarga.TabIndex = 34;
            this.btnCerrarcarga.Text = "Cerrar Carga";
            this.btnCerrarcarga.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrarcarga.UseVisualStyleBackColor = false;
            this.btnCerrarcarga.Click += new System.EventHandler(this.btnCerrarcarga_Click);
            // 
            // Actualizar
            // 
            this.Actualizar.BackColor = System.Drawing.Color.Transparent;
            this.Actualizar.Controls.Add(this.label20);
            this.Actualizar.Controls.Add(this.txtretorno);
            this.Actualizar.Controls.Add(this.txtcargado);
            this.Actualizar.Controls.Add(this.label19);
            this.Actualizar.Controls.Add(this.label18);
            this.Actualizar.Controls.Add(this.txtposition);
            this.Actualizar.Controls.Add(this.txtnp);
            this.Actualizar.Controls.Add(this.label17);
            this.Actualizar.Controls.Add(this.txtnlote);
            this.Actualizar.Controls.Add(this.label16);
            this.Actualizar.Controls.Add(this.btnLimpiar);
            this.Actualizar.Controls.Add(this.txtoidcarga);
            this.Actualizar.Controls.Add(this.label9);
            this.Actualizar.Controls.Add(this.txttotal);
            this.Actualizar.Controls.Add(this.label10);
            this.Actualizar.Controls.Add(this.button3);
            this.Actualizar.Controls.Add(this.btnGuardarAct);
            this.Actualizar.Controls.Add(this.txtship);
            this.Actualizar.Controls.Add(this.label11);
            this.Actualizar.Controls.Add(this.label12);
            this.Actualizar.Controls.Add(this.txtbrach);
            this.Actualizar.Controls.Add(this.txtlotefedex);
            this.Actualizar.Controls.Add(this.label13);
            this.Actualizar.Controls.Add(this.txtpartnumber);
            this.Actualizar.Controls.Add(this.label14);
            this.Actualizar.Controls.Add(this.txtidembarque);
            this.Actualizar.Controls.Add(this.label15);
            this.Actualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Actualizar.ForeColor = System.Drawing.Color.White;
            this.Actualizar.Location = new System.Drawing.Point(12, 335);
            this.Actualizar.Name = "Actualizar";
            this.Actualizar.Size = new System.Drawing.Size(582, 452);
            this.Actualizar.TabIndex = 35;
            this.Actualizar.TabStop = false;
            this.Actualizar.Text = "Retornar";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(176, 316);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(152, 20);
            this.label20.TabIndex = 26;
            this.label20.Text = "Total a Retornar:";
            // 
            // txtretorno
            // 
            this.txtretorno.Enabled = false;
            this.txtretorno.Location = new System.Drawing.Point(182, 339);
            this.txtretorno.Name = "txtretorno";
            this.txtretorno.Size = new System.Drawing.Size(148, 27);
            this.txtretorno.TabIndex = 25;
            // 
            // txtcargado
            // 
            this.txtcargado.Enabled = false;
            this.txtcargado.Location = new System.Drawing.Point(21, 339);
            this.txtcargado.Name = "txtcargado";
            this.txtcargado.Size = new System.Drawing.Size(136, 27);
            this.txtcargado.TabIndex = 24;
            this.txtcargado.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(17, 316);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(85, 20);
            this.label19.TabIndex = 23;
            this.label19.Text = "Cargado:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(356, 253);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 20);
            this.label18.TabIndex = 22;
            this.label18.Text = "Posicion:";
            // 
            // txtposition
            // 
            this.txtposition.Enabled = false;
            this.txtposition.Location = new System.Drawing.Point(360, 276);
            this.txtposition.Name = "txtposition";
            this.txtposition.Size = new System.Drawing.Size(136, 27);
            this.txtposition.TabIndex = 21;
            // 
            // txtnp
            // 
            this.txtnp.Location = new System.Drawing.Point(180, 207);
            this.txtnp.Name = "txtnp";
            this.txtnp.Size = new System.Drawing.Size(153, 27);
            this.txtnp.TabIndex = 20;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(176, 184);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(104, 20);
            this.label17.TabIndex = 19;
            this.label17.Text = "Nuevo  NP:";
            // 
            // txtnlote
            // 
            this.txtnlote.Location = new System.Drawing.Point(360, 207);
            this.txtnlote.Name = "txtnlote";
            this.txtnlote.Size = new System.Drawing.Size(136, 27);
            this.txtnlote.TabIndex = 18;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(356, 184);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(110, 20);
            this.label16.TabIndex = 17;
            this.label16.Text = "Nuevo Lote:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(293, 382);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(130, 51);
            this.btnLimpiar.TabIndex = 16;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtoidcarga
            // 
            this.txtoidcarga.Enabled = false;
            this.txtoidcarga.Location = new System.Drawing.Point(21, 137);
            this.txtoidcarga.Name = "txtoidcarga";
            this.txtoidcarga.Size = new System.Drawing.Size(136, 27);
            this.txtoidcarga.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Id de Carga";
            // 
            // txttotal
            // 
            this.txttotal.Location = new System.Drawing.Point(182, 276);
            this.txttotal.Name = "txttotal";
            this.txttotal.Size = new System.Drawing.Size(151, 27);
            this.txttotal.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(176, 253);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Total a Cargar:";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.RoyalBlue;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(170, 59);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(124, 41);
            this.button3.TabIndex = 11;
            this.button3.Text = "Buscar";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnGuardarAct
            // 
            this.btnGuardarAct.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGuardarAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarAct.ForeColor = System.Drawing.Color.White;
            this.btnGuardarAct.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarAct.Image")));
            this.btnGuardarAct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarAct.Location = new System.Drawing.Point(27, 382);
            this.btnGuardarAct.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardarAct.Name = "btnGuardarAct";
            this.btnGuardarAct.Size = new System.Drawing.Size(130, 51);
            this.btnGuardarAct.TabIndex = 10;
            this.btnGuardarAct.Text = "Guardar";
            this.btnGuardarAct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarAct.UseVisualStyleBackColor = false;
            this.btnGuardarAct.Click += new System.EventHandler(this.btnGuardarAct_Click);
            // 
            // txtship
            // 
            this.txtship.Location = new System.Drawing.Point(21, 276);
            this.txtship.Name = "txtship";
            this.txtship.Size = new System.Drawing.Size(136, 27);
            this.txtship.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 253);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 20);
            this.label11.TabIndex = 8;
            this.label11.Text = "Shipment:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 184);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 20);
            this.label12.TabIndex = 7;
            this.label12.Text = "Brach:";
            // 
            // txtbrach
            // 
            this.txtbrach.Location = new System.Drawing.Point(21, 207);
            this.txtbrach.Name = "txtbrach";
            this.txtbrach.Size = new System.Drawing.Size(136, 27);
            this.txtbrach.TabIndex = 6;
            // 
            // txtlotefedex
            // 
            this.txtlotefedex.Location = new System.Drawing.Point(360, 137);
            this.txtlotefedex.Name = "txtlotefedex";
            this.txtlotefedex.Size = new System.Drawing.Size(136, 27);
            this.txtlotefedex.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(356, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 20);
            this.label13.TabIndex = 4;
            this.label13.Text = "Lote:";
            // 
            // txtpartnumber
            // 
            this.txtpartnumber.Location = new System.Drawing.Point(180, 137);
            this.txtpartnumber.Name = "txtpartnumber";
            this.txtpartnumber.Size = new System.Drawing.Size(153, 27);
            this.txtpartnumber.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(176, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(157, 20);
            this.label14.TabIndex = 2;
            this.label14.Text = "Numero de Parte:";
            // 
            // txtidembarque
            // 
            this.txtidembarque.Location = new System.Drawing.Point(21, 66);
            this.txtidembarque.Name = "txtidembarque";
            this.txtidembarque.Size = new System.Drawing.Size(136, 27);
            this.txtidembarque.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 39);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 20);
            this.label15.TabIndex = 0;
            this.label15.Text = "Id de Embarque";
            // 
            // Cargado
            // 
            this.Cargado.BackColor = System.Drawing.Color.Transparent;
            this.Cargado.Controls.Add(this.dtvgrid);
            this.Cargado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cargado.ForeColor = System.Drawing.Color.Black;
            this.Cargado.Location = new System.Drawing.Point(600, 335);
            this.Cargado.Name = "Cargado";
            this.Cargado.Size = new System.Drawing.Size(588, 336);
            this.Cargado.TabIndex = 36;
            this.Cargado.TabStop = false;
            this.Cargado.Text = "Cargado";
            // 
            // Fedex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.back_screen2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1201, 799);
            this.Controls.Add(this.Cargado);
            this.Controls.Add(this.Actualizar);
            this.Controls.Add(this.btnCerrarcarga);
            this.Controls.Add(this.txttarima2);
            this.Controls.Add(this.txttarima1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtposicion);
            this.Controls.Add(this.txtrestante);
            this.Controls.Add(this.txtcantidad);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtlote);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtenvio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnbuscar);
            this.Controls.Add(this.btnagregar);
            this.Controls.Add(this.btnncarga);
            this.Controls.Add(this.button2);
            this.DoubleBuffered = true;
            this.Name = "Fedex";
            this.Text = "Fedex";
            this.Load += new System.EventHandler(this.Fedex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtvgrid)).EndInit();
            this.Actualizar.ResumeLayout(false);
            this.Actualizar.PerformLayout();
            this.Cargado.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnncarga;
        private System.Windows.Forms.DataGridView dtvgrid;
        private System.Windows.Forms.Button btnagregar;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtenvio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtlote;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtcantidad;
        private System.Windows.Forms.TextBox txtrestante;
        private System.Windows.Forms.TextBox txtposicion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txttarima1;
        private System.Windows.Forms.TextBox txttarima2;
        private System.Windows.Forms.Button btnCerrarcarga;
        private System.Windows.Forms.GroupBox Actualizar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TextBox txtoidcarga;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txttotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnGuardarAct;
        private System.Windows.Forms.TextBox txtship;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtbrach;
        private System.Windows.Forms.TextBox txtlotefedex;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtpartnumber;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtidembarque;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox Cargado;
        private System.Windows.Forms.TextBox txtnp;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtnlote;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtposition;
        private System.Windows.Forms.TextBox txtcargado;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtretorno;
    }
}