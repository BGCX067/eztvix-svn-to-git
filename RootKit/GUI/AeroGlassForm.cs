using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RootKit.GUI
{
    public class AeroGlassForm : System.Windows.Forms.Form
    {

        /// <summary>
        /// Glass Class for transparency windows (aero)
        /// Create a glasse windows using 
        ///      public partial class Form1 : GlassForm 
        ///      {
        ///        private void FormOnLoad(object sender, EventArgs e)
        ///        {
        ///           base.AddGlass(new System.Drawing.Rectangle(0,0, 2000, 2000));
        ///        }
        ///         ...
        ///      }
        /// </summary>

        private struct DwmBlurBehind
        {
            public int Flags;
            public bool Enable;
            public System.IntPtr RgnBlur;
            public bool TransitionOnMaximized;
        }

        // Permet de savoir si l'effet glass est activé.
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private extern static int DwmIsCompositionEnabled(ref int en);

        // Permet d'ajouter un effet glass sur la form
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private extern static void DwmEnableBlurBehindWindow(IntPtr hWnd, ref DwmBlurBehind BlurBehind);

        private int AeroActive = 0; // Si >0 : le theme glass est activé
        private System.Drawing.Region GlassRegion = null; // Tout ce qui est du glass
        private System.Drawing.Region FormRegion = null; // Tout ce qui est de la form
        private System.Drawing.Color _GlassColor = System.Drawing.Color.Black;
        public System.Drawing.Color GlassColor { get { return this._GlassColor; } set { this._GlassColor = value; } }

        public AeroGlassForm()
        {
            // On vérifie si on à bien vista
            if (!base.DesignMode && System.Environment.OSVersion.Version.Major >= 6)
            {
                // Teste si on a le theme glass d'activé
                DwmIsCompositionEnabled(ref AeroActive);

                if (AeroActive > 0)
                {
                    this.FormRegion = new System.Drawing.Region(base.ClientRectangle);
                }
            }
            else
                throw new System.Exception("Windows Vista or upper needed");
        }

        // Permet d'ajouter une zone de glass
        public void AddGlass(System.Drawing.Rectangle Rect)
        {
            if (AeroActive > 0)
            {
                if (this.GlassRegion == null) // Teste si on a déjà une zone de glass
                    this.GlassRegion = new System.Drawing.Region(Rect); // On crée une nouvelle zone de glass
                else
                    this.GlassRegion.Union(Rect); // On ajoute la zone à la region
                this.FormRegion.Exclude(Rect);
                UpdateGlass();
            }
        }
        public void RemoveGlass(System.Drawing.Rectangle Rect)
        {
            if (AeroActive > 0)
            {
                this.GlassRegion.Exclude(Rect); // On retire la zone à la region
                this.FormRegion.Union(Rect);
                UpdateGlass();
            }

        }

        private void UpdateGlass()
        {
            DwmBlurBehind DBB = new DwmBlurBehind();
            DBB.Flags = 0x3;
            DBB.Enable = true;
            DBB.TransitionOnMaximized = false;
            DBB.RgnBlur = this.GlassRegion.GetHrgn(base.CreateGraphics());

            DwmEnableBlurBehindWindow(base.Handle, ref DBB); // Appelle de l'APIs qui ajoute le glass

            base.Invalidate(); // Retrace la form
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs Args)
        {
            if (!(this.GlassRegion == null))
            {
                Args.Graphics.FillRegion(new System.Drawing.SolidBrush(this._GlassColor), this.GlassRegion);
                Args.Graphics.FillRegion(System.Drawing.Brushes.White, this.FormRegion);
            }
            base.OnPaint(Args);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GlassForm
            // 
            this.ClientSize = new System.Drawing.Size(290, 270);
            this.Name = "GlassForm";
            this.Load += new System.EventHandler(this.GlassForm_Load);
            this.ResumeLayout(false);

        }

        private void GlassForm_Load(object sender, EventArgs e)
        {

        }

    }

}
