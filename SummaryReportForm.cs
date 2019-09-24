// ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NepTrans
{
    public partial class SummaryReportForm : Form
    {
        NepEntryManager EntryManager { get; set; }

        // cache;
#if false
        static int game = 6768;
        static int system = 13441;
        static int overall = game + system;
        static int cgame = 3 + 3333;
        static int csystem = 41 + 6500;
        static int coverall = cgame + csystem;
        //static float gpercent = (float)game / overall;
#else
        int game = 0;
        int system = 0;
        int overall = 0;
        int cgame = 0;
        int csystem = 0;
        int coverall = 0;
#endif

        public SummaryReportForm(NepEntryManager _entryManager)
        {
            InitializeComponent();

            EntryManager = _entryManager;

            //pnlReport.Invalidate();
            //pnlReport.Update();

            UpdateProgressDisplay();
        }

        public bool UpdateProgressDisplay()
        {
            if (EntryManager == null)
            {
                Console.WriteLine("Invalid EntryManager.");
                return false;
            }

            game = EntryManager.GameScriptRecordCount;
            system = EntryManager.SystemScriptRecordCount;
            overall = game + system;
            cgame = EntryManager.GameScriptCompletedRecordCount;
            csystem = EntryManager.SystemScriptCompletedRecordCount;
            coverall = cgame + csystem;

            lblOverall.Text = string.Format("{0}/{1} ({2:F2}%)", coverall, overall, (float)coverall / overall * 100);
            lblGameScript.Text = string.Format("{0}/{1} ({2:F2}%)", cgame, game, (float)cgame / game * 100);
            lblSystemScript.Text = string.Format("{0}/{1} ({2:F2}%)", csystem, system, (float)csystem / system * 100);

            pnlReport.Invalidate();

            return true;
        }

        private void pnlReport_Paint(object sender, PaintEventArgs e)
        {
            if (EntryManager == null)
            {
                Console.WriteLine("SummaryReportForm.pnlReport_Paint() -> Invalid EntryManager.");
                //return;
            }
            
            float gpercent = (float)game / overall;

            // FillPie() startAngle starts from 3h position;
            float startAngle = -90.0f;
            float seperateLine = gpercent * 360.0f;
            float gameCompleteAngle = (float)cgame / game * seperateLine;
            float systemCompleteAngle = (float)csystem / system * (seperateLine - 360.0f);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Brush gProgressBrush = new SolidBrush(Color.LightSeaGreen);
            Brush sProgressBrush = new SolidBrush(Color.LightSkyBlue);
            Pen outlinePen = new Pen(Color.Black, 1.0f);

            // hack: pnlReport is always square shaped;
            int px = 50;
            int py = 50;
            int pw = pnlReport.Width - px - px;
            int ph = pnlReport.Height - py - py;
            Rectangle pieBound = new Rectangle(px, py, pw, ph);

            g.FillPie(gProgressBrush, pieBound, startAngle, gameCompleteAngle);
            g.FillPie(sProgressBrush, pieBound, startAngle, systemCompleteAngle);
            g.DrawPie(outlinePen, pieBound, startAngle, seperateLine);
            g.DrawPie(outlinePen, pieBound, startAngle, seperateLine - 360.0f);

            int rsize = 15;
            Rectangle gnote = new Rectangle(pnlReport.Width - rsize - 1, lblGameScriptHead.Location.Y + 1, rsize, rsize);
            Rectangle snote = new Rectangle(pnlReport.Width - rsize - 1, lblSystemScriptHead.Location.Y + 1, rsize, rsize);
            g.FillRectangle(gProgressBrush, gnote);
            g.FillRectangle(sProgressBrush, snote);
            g.DrawRectangle(outlinePen, gnote);
            g.DrawRectangle(outlinePen, snote);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
