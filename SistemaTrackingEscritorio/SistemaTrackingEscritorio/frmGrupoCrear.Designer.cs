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
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarioBusqueda)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNombreGrupo
            // 
            this.tbNombreGrupo.Location = new System.Drawing.Point(194, 26);
            this.tbNombreGrupo.Name = "tbNombreGrupo";
            this.tbNombreGrupo.Size = new System.Drawing.Size(100, 20);
            this.tbNombreGrupo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre del grupo";
            // 
            // dgvUsuarioBusqueda
            // 
            this.dgvUsuarioBusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarioBusqueda.Location = new System.Drawing.Point(29, 74);
            this.dgvUsuarioBusqueda.Name = "dgvUsuarioBusqueda";
            this.dgvUsuarioBusqueda.Size = new System.Drawing.Size(197, 177);
            this.dgvUsuarioBusqueda.TabIndex = 4;
            // 
            // btnCrearGrupo
            // 
            this.btnCrearGrupo.Location = new System.Drawing.Point(292, 101);
            this.btnCrearGrupo.Name = "btnCrearGrupo";
            this.btnCrearGrupo.Size = new System.Drawing.Size(75, 23);
            this.btnCrearGrupo.TabIndex = 9;
            this.btnCrearGrupo.Text = "Crear";
            this.btnCrearGrupo.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(292, 187);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // frmGrupoCrear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 299);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrearGrupo);
            this.Controls.Add(this.dgvUsuarioBusqueda);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNombreGrupo);
            this.Name = "frmGrupoCrear";
            this.Text = "frmGrupoCrear";
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
    }
}