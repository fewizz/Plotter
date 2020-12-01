using Parser;
using System.Windows.Forms;

namespace Plotter
{
    public partial class PlainGridControl : GridControl
    {
        public PlainGridControl()
        {
            Init(new PlainGrid());
            InitializeComponent();

            Load += (s, e) =>
              {
                  Step.StatusUpdater = () => (Grid as PlainGrid).TryParseStep(Step.Text);

                  Step.Text = "0.5";
                  colorControl1[ColorComponent.Red].Text = "clamp(y*1.5, 0, 1)*normal_y";
                  colorControl1[ColorComponent.Green].Text = "clamp(1.5-|y|, 0, 1)*normal_y";
                  colorControl1[ColorComponent.Blue].Text = "clamp(-y*1.5, 0, 1)*normal_y";
                  colorControl1[ColorComponent.Alpha].Text = "1";
              };
        }
    }
}
