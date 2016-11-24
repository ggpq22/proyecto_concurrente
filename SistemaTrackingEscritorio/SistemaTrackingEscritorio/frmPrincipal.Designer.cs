namespace Mapa
{
    partial class frmPrincipal
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
            this.btnGrupos = new System.Windows.Forms.Button();
            this.mapa = new GMap.NET.WindowsForms.GMapControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvUsuariosGrupo = new System.Windows.Forms.DataGridView();
            this.pbProgreso = new System.Windows.Forms.ProgressBar();
            this.dgvGruposAnfitrion = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbZoom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuariosGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGruposAnfitrion)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGrupos
            // 
            this.btnGrupos.BackgroundImage = global::Mapa.Properties.Resources.personas;
            this.btnGrupos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrupos.Location = new System.Drawing.Point(1046, 147);
            this.btnGrupos.Name = "btnGrupos";
            this.btnGrupos.Size = new System.Drawing.Size(75, 68);
            this.btnGrupos.TabIndex = 0;
            this.btnGrupos.UseVisualStyleBackColor = true;
            this.btnGrupos.Click += new System.EventHandler(this.btnGrupos_Click);
            // 
            // mapa
            // 
            this.mapa.Bearing = 0F;
            this.mapa.CanDragMap = true;
            this.mapa.EmptyTileColor = System.Drawing.Color.Navy;
            this.mapa.GrayScaleMode = false;
            this.mapa.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mapa.LevelsKeepInMemmory = 5;
            this.mapa.Location = new System.Drawing.Point(23, 92);
            this.mapa.MarkersEnabled = true;
            this.mapa.MaxZoom = 2;
            this.mapa.MinZoom = 2;
            this.mapa.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapa.Name = "mapa";
            this.mapa.NegativeMode = false;
            this.mapa.PolygonsEnabled = true;
            this.mapa.RetryLoadTile = 0;
            this.mapa.RoutesEnabled = true;
            this.mapa.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mapa.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mapa.ShowTileGridLines = false;
            this.mapa.Size = new System.Drawing.Size(625, 416);
            this.mapa.TabIndex = 1;
            this.mapa.Zoom = 0D;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(788, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Grupos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(786, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Usuario";
            // 
            // dgvUsuariosGrupo
            // 
            this.dgvUsuariosGrupo.AllowUserToAddRows = false;
            this.dgvUsuariosGrupo.AllowUserToDeleteRows = false;
            this.dgvUsuariosGrupo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuariosGrupo.Location = new System.Drawing.Point(708, 377);
            this.dgvUsuariosGrupo.Name = "dgvUsuariosGrupo";
            this.dgvUsuariosGrupo.ReadOnly = true;
            this.dgvUsuariosGrupo.RowHeadersVisible = false;
            this.dgvUsuariosGrupo.Size = new System.Drawing.Size(250, 131);
            this.dgvUsuariosGrupo.TabIndex = 4;
            this.dgvUsuariosGrupo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuariosGrupo_CellContentClick);
            // 
            // pbProgreso
            // 
            this.pbProgreso.Location = new System.Drawing.Point(255, 570);
            this.pbProgreso.Name = "pbProgreso";
            this.pbProgreso.Size = new System.Drawing.Size(837, 23);
            this.pbProgreso.TabIndex = 7;
            this.pbProgreso.Visible = false;
            // 
            // dgvGruposAnfitrion
            // 
            this.dgvGruposAnfitrion.AllowUserToAddRows = false;
            this.dgvGruposAnfitrion.AllowUserToDeleteRows = false;
            this.dgvGruposAnfitrion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGruposAnfitrion.Location = new System.Drawing.Point(708, 123);
            this.dgvGruposAnfitrion.Name = "dgvGruposAnfitrion";
            this.dgvGruposAnfitrion.ReadOnly = true;
            this.dgvGruposAnfitrion.RowHeadersVisible = false;
            this.dgvGruposAnfitrion.Size = new System.Drawing.Size(250, 150);
            this.dgvGruposAnfitrion.TabIndex = 9;
            this.dgvGruposAnfitrion.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGruposAnfitrion_CellClick);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackgroundImage = global::Mapa.Properties.Resources.engranaje;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.Location = new System.Drawing.Point(1046, 407);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 68);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(446, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(309, 37);
            this.label3.TabIndex = 11;
            this.label3.Text = "Sistema de Tracking";
            // 
            // tbZoom
            // 
            this.tbZoom.Location = new System.Drawing.Point(817, 523);
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(32, 20);
            this.tbZoom.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(748, 521);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "zoom";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = global::Mapa.Properties.Resources.cancelar;
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.Location = new System.Drawing.Point(1046, 269);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 68);
            this.btnEliminar.TabIndex = 14;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(964, 523);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 29);
            this.button1.TabIndex = 15;
            this.button1.Text = "prueba";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mapa.Properties.Resources.images__1_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1201, 624);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbZoom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvGruposAnfitrion);
            this.Controls.Add(this.pbProgreso);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvUsuariosGrupo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapa);
            this.Controls.Add(this.btnGrupos);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuariosGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGruposAnfitrion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.Button btnGrupos;
        private GMap.NET.WindowsForms.GMapControl mapa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvUsuariosGrupo;
        private System.Windows.Forms.ProgressBar pbProgreso;
        internal System.Windows.Forms.DataGridView dgvGruposAnfitrion;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbZoom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button button1;
    }
}