using Parser;

namespace Plotter
{
    public partial class PlainGridControl : GridControl
    {
        public PlainGridControl()
        {
            Init(new PlainGrid());
            InitializeComponent();
            Step.StatusUpdater = () =>
            {
                //IExpression e = Parser.Parser.TryParse(Step.Text, new object[0]);
                //if (e == null) return Status.Error;
                //(Grid as PlainGrid).Step = (float)e.Value;
                //Grid.TryParseValueExpression(expression.Text);
                //return Status.Ok;
                return (Grid as PlainGrid).TryParseStep(Step.Text);
            };
            Step.Text = "0.5";
        }
    }
}
