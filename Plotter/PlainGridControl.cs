using Parser;

namespace Plotter
{
    public partial class PlainGridControl : GridControl
    {
        public PlainGridControl()
        {
            Init(new PlainGrid());
            InitializeComponent();
            Step.StatusUpdater = () => (Grid as PlainGrid).TryParseStep(Step.Text);

            Step.Text = "0.5";
            colorControl1[ColorComponent.Red].Text = "y*1.5*normal_y";
            colorControl1[ColorComponent.Green].Text = "(1.5-|y|)*normal_y";
            colorControl1[ColorComponent.Blue].Text = "(-y*1.5)*normal_y";
            colorControl1[ColorComponent.Alpha].Text = "1";
        }
    }
}
