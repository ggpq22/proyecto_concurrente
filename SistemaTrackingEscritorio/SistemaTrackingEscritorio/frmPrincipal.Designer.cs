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
            this.dgvGruposAnfitrion = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvUsuariosGrupo = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.pbProgreso = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGruposAnfitrion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuariosGrupo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGrupos
            // 
            this.btnGrupos.Location = new System.Drawing.Point(675, 405);
            this.btnGrupos.Name = "btnGrupos";
            this.btnGrupos.Size = new System.Drawing.Size(75, 23);
            this.btnGrupos.TabIndex = 0;
            this.btnGrupos.Text = "Crear Grupo";
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
            this.mapa.Location = new System.Drawing.Point(12, 12);
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
            // dgvGruposAnfitrion
            // 
            this.dgvGruposAnfitrion.AllowUserToAddRows = false;
            this.dgvGruposAnfitrion.AllowUserToDeleteRows = false;
            this.dgvGruposAnfitrion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGruposAnfitrion.Location = new System.Drawing.Point(654, 30);
            this.dgvGruposAnfitrion.Name = "dgvGruposAnfitrion";
            this.dgvGruposAnfitrion.ReadOnly = true;
            this.dgvGruposAnfitrion.RowHeadersVisible = false;
            this.dgvGruposAnfitrion.Size = new System.Drawing.Size(250, 141);
            this.dgvGruposAnfitrion.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(758, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Grupos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(758, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Usuario";
            // 
            // dgvUsuariosGrupo
            // 
            this.dgvUsuariosGrupo.AllowUserToAddRows = false;
            this.dgvUsuariosGrupo.AllowUserToDeleteRows = false;
            this.dgvUsuariosGrupo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuariosGrupo.Location = new System.Drawing.Point(654, 229);
            this.dgvUsuariosGrupo.Name = "dgvUsuariosGrupo";
            this.dgvUsuariosGrupo.ReadOnly = true;
            this.dgvUsuariosGrupo.RowHeadersVisible = false;
            this.dgvUsuariosGrupo.Size = new System.Drawing.Size(250, 131);
            this.dgvUsuariosGrupo.TabIndex = 4;
            this.dgvUsuariosGrupo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuariosGrupo_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(794, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pbProgreso
            // 
            this.pbProgreso.Location = new System.Drawing.Point(196, 448);
            this.pbProgreso.Name = "pbProgreso";
            this.pbProgreso.Size = new System.Drawing.Size(501, 23);
            this.pbProgreso.TabIndex = 7;
            this.pbProgreso.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(794, 447);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 492);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pbProgreso);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvUsuariosGrupo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvGruposAnfitrion);
            this.Controls.Add(this.mapa);
            this.Controls.Add(this.btnGrupos);
            this.Name = "frmPrincipal";
            this.Text = "frmPrincipal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGruposAnfitrion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuariosGrupo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapa;
        private System.Windows.Forms.Button btnGrupos;
        private GMap.NET.WindowsForms.GMapControl mapa;
        private System.Windows.Forms.DataGridView dgvGruposAnfitrion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvUsuariosGrupo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar pbProgreso;
        private System.Windows.Forms.Button button2;
    }
}