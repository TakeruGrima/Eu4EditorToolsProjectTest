namespace Eu4ProvincePicker
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelPicture1 = new Eu4ProvincePicker.PanelPicture();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FindBox = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPicture1
            // 
            this.panelPicture1.AutoScroll = true;
            this.panelPicture1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPicture1.Image = ((System.Drawing.Image)(resources.GetObject("panelPicture1.Image")));
            this.panelPicture1.Location = new System.Drawing.Point(0, 27);
            this.panelPicture1.Name = "panelPicture1";
            this.panelPicture1.Size = new System.Drawing.Size(984, 714);
            this.panelPicture1.TabIndex = 0;
            this.panelPicture1.Click += new System.EventHandler(this.panelPicture1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindBox});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 27);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FindBox
            // 
            this.FindBox.Name = "FindBox";
            this.FindBox.Size = new System.Drawing.Size(100, 23);
            this.FindBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(984, 741);
            this.Controls.Add(this.panelPicture1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PanelPicture panelPicture1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox FindBox;
    }
}

