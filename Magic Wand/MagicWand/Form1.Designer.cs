namespace MagicWand
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
            this.pixelPictureBox1 = new MagicWand.PixelPictureBox();
            this.pixelPictureBox2 = new MagicWand.PixelPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pixelPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelPictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pixelPictureBox1
            // 
            this.pixelPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pixelPictureBox1.Image")));
            this.pixelPictureBox1.Location = new System.Drawing.Point(11, 12);
            this.pixelPictureBox1.Name = "pixelPictureBox1";
            this.pixelPictureBox1.Size = new System.Drawing.Size(260, 240);
            this.pixelPictureBox1.TabIndex = 0;
            this.pixelPictureBox1.TabStop = false;
            // 
            // pixelPictureBox2
            // 
            this.pixelPictureBox2.Location = new System.Drawing.Point(277, 12);
            this.pixelPictureBox2.Name = "pixelPictureBox2";
            this.pixelPictureBox2.Size = new System.Drawing.Size(260, 240);
            this.pixelPictureBox2.TabIndex = 1;
            this.pixelPictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 261);
            this.Controls.Add(this.pixelPictureBox2);
            this.Controls.Add(this.pixelPictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pixelPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelPictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PixelPictureBox pixelPictureBox1;
        private PixelPictureBox pixelPictureBox2;
    }
}

