using Reqnroll;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class UsingCalculatorAvailabilitySteps
{
    private readonly CalculatorContext _context;
    private readonly ReliabilityContext _reliability;

    public UsingCalculatorAvailabilitySteps(CalculatorContext context, ReliabilityContext reliability)
    {
        _context = context;
        _reliability = reliability;
    }

    [When("I have entered {double} and {double} into the calculator and press MTBF")]
    public void WhenIHaveEnteredAndPressMtbf(double operatingTime, double failures)
    {
        _context.Result = null;
        _context.Error = null;

        try
        {
            _context.Result = _context.Calculator.Mtbf(operatingTime, failures);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }

    [When("I have entered {double} and {double} into the calculator and press Availability")]
    public void WhenIHaveEnteredAndPressAvailability(double mtbf, double mttr)
    {
        _context.Result = null;
        _context.Error = null;

        try
        {
            _context.Result = _context.Calculator.Availability(mtbf, mttr);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }

    [Given("the reliability values are")]
    public void GivenTheReliabilityValuesAre(DataTable table)
    {
        var values = table.Rows[0];

        _reliability.Mtbf = double.Parse(values["MTBF"]);
        _reliability.Mttr = double.Parse(values["MTTR"]);
    }

    [When("I calculate Availability from these values")]
    public void WhenICalculateAvailabilityFromTheseValues()
    {
        _context.Result = _context.Calculator.Availability(_reliability.Mtbf, _reliability.Mttr);
    }
}