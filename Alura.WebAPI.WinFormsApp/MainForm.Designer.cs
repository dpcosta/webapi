namespace Alura.WebAPI.WinFormsApp
{
    partial class MainForm
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
            this.TipoListaCombo = new System.Windows.Forms.ComboBox();
            this.LivrosListBox = new System.Windows.Forms.ListBox();
            this.DetalhesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TipoListaCombo
            // 
            this.TipoListaCombo.FormattingEnabled = true;
            this.TipoListaCombo.Items.AddRange(new object[] {
            "Para Ler",
            "Lendo",
            "Lidos"});
            this.TipoListaCombo.Location = new System.Drawing.Point(30, 12);
            this.TipoListaCombo.Name = "TipoListaCombo";
            this.TipoListaCombo.Size = new System.Drawing.Size(191, 21);
            this.TipoListaCombo.TabIndex = 0;
            this.TipoListaCombo.SelectedIndexChanged += new System.EventHandler(this.TipoListaCombo_SelectedIndexChanged);
            // 
            // LivrosListBox
            // 
            this.LivrosListBox.FormattingEnabled = true;
            this.LivrosListBox.Location = new System.Drawing.Point(31, 39);
            this.LivrosListBox.Name = "LivrosListBox";
            this.LivrosListBox.Size = new System.Drawing.Size(190, 134);
            this.LivrosListBox.TabIndex = 1;
            this.LivrosListBox.SelectedIndexChanged += new System.EventHandler(this.LivrosListBox_SelectedIndexChanged);
            // 
            // DetalhesButton
            // 
            this.DetalhesButton.Enabled = false;
            this.DetalhesButton.Location = new System.Drawing.Point(31, 179);
            this.DetalhesButton.Name = "DetalhesButton";
            this.DetalhesButton.Size = new System.Drawing.Size(190, 30);
            this.DetalhesButton.TabIndex = 2;
            this.DetalhesButton.Text = "Detalhes";
            this.DetalhesButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 240);
            this.Controls.Add(this.DetalhesButton);
            this.Controls.Add(this.LivrosListBox);
            this.Controls.Add(this.TipoListaCombo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Leitura";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox TipoListaCombo;
        private System.Windows.Forms.ListBox LivrosListBox;
        private System.Windows.Forms.Button DetalhesButton;
    }
}

