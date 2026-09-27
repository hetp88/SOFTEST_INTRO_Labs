using SOFTEST_INTRO_Calculator;
using NUnit.Framework;

namespace SOFTEST_INTRO_Calculator.UnitTests;

public class CalculatorTests
{
  private Calculator _calculator = null!;
  
  [SetUp]
  public void SetUp()
  {
    _calculator = new Calculator();
  }

  // --- Addition ---
  [Test]
  public void Add_TwoPositiveNumbers_ReturnsSum()
  {
    // Arrange: the calculator is created in SetUp.
    // Act
    double result = _calculator.Add(10, 20);
    // Assert
    Assert.That(result, Is.EqualTo(30));
  }

  [TestCase(0, 0, 0)]
  [TestCase(0, 5, 5)]
  [TestCase(-3, 8, 5)]
  [TestCase(0.1, 0.2, 0.3)]
  public void Add_RepresentativeInputs_ReturnsSum(double a, double b, double expected)
  {
    double result = _calculator.Add(a, b);
    Assert.That(result, Is.EqualTo(expected).Within(1e-9));
  }

  // --- Subtraction ---
  // Cases chosen to catch: swapped operands (a - b vs b - a),
  // a sign error, and an implementation that mishandles zero.

  [TestCase(10, 4, 6)]     // normal case
  [TestCase(5, 5, 0)]      // zero result
  [TestCase(-3, -8, 5)]    // negative operands
  [TestCase(0, 7, -7)]     // zero minuend, catches operand-order bugs
  public void Subtract_RepresentativeInputs_ReturnsDifference(
    double a, double b, double expected)
  {
    double result = _calculator.Subtract(a, b);
    Assert.That(result, Is.EqualTo(expected).Within(1e-9));
  }

  // --- Multiplication ---
  // Cases chosen to catch: an addition-instead-of-multiplication bug,
  // failure to handle zero, and a sign error.

  [TestCase(4, 5, 20)]     // normal case
  [TestCase(0, 9, 0)]      // zero factor
  [TestCase(-3, 6, -18)]   // one negative operand
  [TestCase(-2, -7, 14)]   // two negative operands
  public void Multiply_RepresentativeInputs_ReturnsProduct(
    double a, double b, double expected)
  {
    double result = _calculator.Multiply(a, b);
    Assert.That(result, Is.EqualTo(expected).Within(1e-9));
  }

  // --- Division: valid cases (from the contract table) ---

  [TestCase(1, 2, 0.5)]
  [TestCase(0, 15, 0)]
  [TestCase(15, -3, -5)]
  public void Divide_ValidInputs_ReturnsQuotient(
    double a, double b, double expected)
  {
    double result = _calculator.Divide(a, b);
    Assert.That(result, Is.EqualTo(expected).Within(1e-9));
  }

  // --- Division: zero-divisor rule ---

  [TestCase(15, 0)]
  [TestCase(0, 0)]
  public void Divide_ZeroDivisor_ThrowsArgumentException(double a, double b)
  {
    Assert.That(() => _calculator.Divide(a, b),
        Throws.TypeOf<ArgumentException>());
  }
  
  [Test]
  public void Factorial_Zero_ReturnsOne()
  {
    long result = _calculator.Factorial(0);
    Assert.That(result, Is.EqualTo(1L));
  }

  [TestCase(5, 5, 120L)]
  [TestCase(5, 4, 120L)]
  [TestCase(5, 3, 60L)]
  [TestCase(5, 0, 1L)]
  [TestCase(0, 0, 1L)]
  public void UnknownFunctionA_ValidInputs_ReturnsExpectedValue(int n, int r, long expected)
  {
    long result = _calculator.UnknownFunctionA(n, r);
    Assert.That(result, Is.EqualTo(expected));
  }

  [TestCase(-4, 5)]
  [TestCase(4, 5)]
  public void UnknownFunctionA_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
  {
    Assert.That(() => _calculator.UnknownFunctionA(n, r),
      Throws.TypeOf<ArgumentOutOfRangeException>());
  }

  [TestCase(5, 5, 1L)]
  [TestCase(5, 4, 5L)]
  [TestCase(5, 3, 10L)]
  [TestCase(5, 0, 1L)]
  [TestCase(0, 0, 1L)]
  public void UnknownFunctionB_ValidInputs_ReturnsExpectedValue(int n, int r, long expected)
  {
    long result = _calculator.UnknownFunctionB(n, r);
    Assert.That(result, Is.EqualTo(expected));
  }

  [TestCase(-4, 5)]
  [TestCase(4, 5)]
  public void UnknownFunctionB_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
  {
    Assert.That(() => _calculator.UnknownFunctionB(n, r),
      Throws.TypeOf<ArgumentOutOfRangeException>());
  }

  // S7: special-case Add
  [TestCase(1, 11, 7)]
  [TestCase(10, 11, 11)]
  [TestCase(11, 11, 15)]
  public void Add_BinaryDigitOperands_ReturnsLabSpecialCase(double a, double b, double expected)
  {
    Assert.That(_calculator.Add(a, b), Is.EqualTo(expected));
  }

  // S12: MTBF
  [Test]
  public void Mtbf_NormalInput_ReturnsAverage()
  {
    Assert.That(_calculator.Mtbf(100, 4), Is.EqualTo(25).Within(1e-9));
  }

  [TestCase(0, 1)]
  [TestCase(100, 0)]
  [TestCase(-1, 1)]
  public void Mtbf_InvalidInput_Throws(double time, double failures)
  {
    Assert.That(() => _calculator.Mtbf(time, failures), Throws.TypeOf<ArgumentOutOfRangeException>());
  }

  // S12: Availability
  [TestCase(90, 10, 0.9)]
  [TestCase(80, 20, 0.8)]
  [TestCase(0, 10, 0)]
  public void Availability_ValidInput_ReturnsRatio(double mtbf, double mttr, double expected)
  {
    Assert.That(_calculator.Availability(mtbf, mttr), Is.EqualTo(expected).Within(1e-9));
  }

  [TestCase(-1, 10)]
  [TestCase(10, -1)]
  [TestCase(0, 0)]
  public void Availability_InvalidInput_Throws(double mtbf, double mttr)
  {
    Assert.That(() => _calculator.Availability(mtbf, mttr), Throws.TypeOf<ArgumentOutOfRangeException>());
  }

  // S14: Basic Musa
  [Test]
  public void Musa_TauZero_IntensityEqualsInitialAndFailuresZero()
  {
    Assert.That(_calculator.CurrentFailureIntensity(10, 100, 0), Is.EqualTo(10).Within(1e-9));
    Assert.That(_calculator.ExpectedCumulativeFailures(10, 100, 0), Is.EqualTo(0).Within(1e-9));
  }

  [Test]
  public void Musa_NormalTau_ReturnsExpectedValues()
  {
    Assert.That(_calculator.CurrentFailureIntensity(10, 100, 10), Is.EqualTo(3.678794412).Within(1e-6));
    Assert.That(_calculator.ExpectedCumulativeFailures(10, 100, 10), Is.EqualTo(63.212055883).Within(1e-6));
  }

  [TestCase(0, 100, 10)]
  [TestCase(10, 0, 10)]
  [TestCase(10, 100, -1)]
  public void Musa_InvalidInput_Throws(double lambda0, double nu0, double tau)
  {
    Assert.That(() => _calculator.CurrentFailureIntensity(lambda0, nu0, tau),
      Throws.TypeOf<ArgumentOutOfRangeException>());
    Assert.That(() => _calculator.ExpectedCumulativeFailures(lambda0, nu0, tau),
      Throws.TypeOf<ArgumentOutOfRangeException>());
  }
}