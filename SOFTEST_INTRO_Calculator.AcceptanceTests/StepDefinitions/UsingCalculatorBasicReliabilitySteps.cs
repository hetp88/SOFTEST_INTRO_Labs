using Reqnroll;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class UsingCalculatorBasicReliabilitySteps
{
  private readonly CalculatorContext _context;
  private readonly ReliabilityContext _reliability;

  public UsingCalculatorBasicReliabilitySteps(CalculatorContext context, ReliabilityContext reliability)
  {
    _context = context;
    _reliability = reliability;
  }

  [Given("the Basic Musa parameters are")]
  public void GivenTheBasicMusaParametersAre(DataTable table)
  {
    var values = table.Rows[0];

    _reliability.Lambda0 = double.Parse(values["Lambda0"]);
    _reliability.Nu0 = double.Parse(values["Nu0"]);
    _reliability.Tau = double.Parse(values["Tau"]);
  }

  [When("I calculate the current failure intensity")]
  public void WhenICalculateTheCurrentFailureIntensity()
  {
    _context.Result = null;
    _context.Error = null;

    try
    {
      _context.Result = _context.Calculator.CurrentFailureIntensity(
          _reliability.Lambda0, _reliability.Nu0, _reliability.Tau);
    }
    catch (ArgumentOutOfRangeException error)
    {
      _context.Error = error;
    }
  }

  [When("I calculate the expected cumulative failures")]
  public void WhenICalculateTheExpectedCumulativeFailures()
  {
    _context.Result = null;
    _context.Error = null;

    try
    {
      _context.Result = _context.Calculator.ExpectedCumulativeFailures(
          _reliability.Lambda0, _reliability.Nu0, _reliability.Tau);
    }
    catch (ArgumentOutOfRangeException error)
    {
      _context.Error = error;
    }
  }
}