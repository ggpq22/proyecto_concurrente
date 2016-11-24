namespace Mapa
{
    partial class frmGrupoCrear
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
            this.tbNombreGrupo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvUsuarioBusqueda = new System.Windows.Forms.DataGridView();
            this.btnCrearGrupo = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.prProgreso = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarioBusqueda)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNombreGrupo
            // 
            this.tbNombreGrupo.Location = new System.Drawing.Point(321, 40);
            this.tbNombreGrupo.Name = "tbNombreGrupo";
            this.tbNombreGrupo.Size = new System.Drawing.Size(154, 20);
            this.tbNombreGrupo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(202, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre del grupo";
            // 
            // dgvUsuarioBusqueda
            // 
            this.dgvUsuarioBusqueda.AllowUserToAddRows = false;
            this.dgvUsuarioBusqueda.AllowUserToDeleteRows = false;
            this.dgvUsuarioBusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarioBusqueda.Location = new System.Drawing.Point(106, 102);
            this.dgvUsuarioBusqueda.Name = "dgvUsuarioBusqueda";
            this.dgvUsuarioBusqueda.ReadOnly = true;
            this.dgvUsuarioBusqueda.RowHeadersVisible = false;
            this.dgvUsuarioBusqueda.Size = new System.Drawing.Size(197, 177);
            this.dgvUsuarioBusqueda.TabIndex = 4;
            // 
            // btnCrearGrupo
            // 
            this.btnCrearGrupo.BackgroundImage = global::Mapa.Properties.Resources.anadir;
            this.btnCrearGrupo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCrearGrupo.Location = new System.Drawing.Point(429, 127);
            this.btnCrearGrupo.Name = "btnCrearGrupo";
            this.btnCrearGrupo.Size = new System.Drawing.Size(46, 41);
            this.btnCrearGrupo.TabIndex = 9;
            this.btnCrearGrupo.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackgroundImage = global::Mapa.Properties.Resources.cancelar;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelar.Location = new System.Drawing.Point(429, 209);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(46, 41);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // prProgreso
            // 
            this.prProgreso.Location = new System.Drawing.Point(183, 333);
            this.prProgreso.Name = "prProgreso";
            this.prProgreso.Size = new System.Drawing.Size(308, 23);
            this.prProgreso.TabIndex = 11;
            this.prProgreso.Visible = false;
            // 
            // frmGrupoCrear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mapa.Properties.Resources.images__1_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(596, 399);
            this.Controls.Add(this.prProgreso);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrearGrupo);
            this.Controls.Add(this.dgvUsuarioBusqueda);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNombreGrupo);
            this.Name = "frmGrupoCrear";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crear Grupo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGrupoCrear_FormClosing);
            this.Load += new System.EventHandler(this.frmGrupoCrear_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarioBusqueda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNombreGrupo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvUsuarioBusqueda;
        private System.Windows.Forms.Button btnCrearGrupo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ProgressBar prProgreso;
    }
}