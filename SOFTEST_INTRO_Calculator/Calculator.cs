namespace SOFTEST_INTRO_Calculator;
using System.Globalization;

public class Calculator
{
	private static bool TryBinaryConcatenation(double a, double b, out double result)
	{
		result = 0;

		if (a <= 0 || b <= 0 || a != Math.Floor(a) || b != Math.Floor(b))
		{
			return false;
		}
		string combined = a.ToString("R", CultureInfo.InvariantCulture) + b.ToString("R", CultureInfo.InvariantCulture);
		
		if (combined.Length > 62 || combined.Any(c => c != '0' && c != '1'))
		{
			return false;
		}
		result = Convert.ToInt64(combined, 2);
		return true;
	}
	public double Add(double a, double b)
	{
		if (TryBinaryConcatenation(a, b, out double special))
		{
			return special;
		}
		return a + b;
	}

	public double Subtract(double a, double b) => a - b;

	public double Multiply(double a, double b) => a * b;

	// Starter version: complete the zero-divisor rule in section 5.
	public double Divide(double a, double b)
	{
		if (b == 0)
			throw new ArgumentException("Cannot divide by zero.");
		return a / b;
	}
	
	public double DoOperation(double a, double b, string op)
	{
		return op switch
		{
			"a" => Add(a, b),
			"s" => Subtract(a, b),
			"m" => Multiply(a, b),
			"d" => Divide(a, b),
			_ => throw new ArgumentException("Unknown operation.")
		};
	}

	public long Factorial(int n)
	{
		if (n < 0 || n > 20)
			throw new ArgumentOutOfRangeException(nameof(n));
		long result = 1;
		for (int i = 2; i <= n; i++)
		{
			result *= i;
		}
		return result;
	}

	public long UnknownFunctionA(int n, int r)
	{
		if (r < 0 || n < 0 || r > n)
			throw new ArgumentOutOfRangeException();

		return Factorial(n) / Factorial(n - r);
	}

	public long UnknownFunctionB(int n, int r)
	{
		if (r < 0 || n < 0 || r > n)
			throw new ArgumentOutOfRangeException();

		return Factorial(n) / (Factorial(r) * Factorial(n - r));
	}

	public double Mtbf(double operatingTime, double failures)
	{	
		if (operatingTime <= 0)
			throw new ArgumentOutOfRangeException(nameof(operatingTime), "Operating time must be positive.");
		if (failures <= 0)
			throw new ArgumentOutOfRangeException(nameof(failures), "Failure count must be positive.");
		return operatingTime / failures;
	}

	public double Availability(double mtbf, double mttr)
	{
		if (mtbf < 0)
			throw new ArgumentOutOfRangeException(nameof(mtbf), "MTBF cannot be negative.");
		if (mttr < 0)
			throw new ArgumentOutOfRangeException(nameof(mttr), "MTTR cannot be negative.");
		if (mtbf + mttr <= 0)
			throw new ArgumentOutOfRangeException(nameof(mtbf), "MTBF + MTTR must be positive.");
		return mtbf / (mtbf + mttr);
	}

	public double CurrentFailureIntensity(double lambda0, double nu0, double tau)
	{
		ValidateMusa(lambda0, nu0, tau);
		return lambda0 * Math.Exp(-lambda0 * tau / nu0);
	}

	public double ExpectedCumulativeFailures(double lambda0, double nu0, double tau)
	{
		ValidateMusa(lambda0, nu0, tau);
		return nu0 * (1 - Math.Exp(-lambda0 * tau / nu0));
	}

	private static void ValidateMusa(double lambda0, double nu0, double tau)
	{
		if (lambda0 <= 0)
			throw new ArgumentOutOfRangeException(nameof(lambda0), "Initial failure intensity must be > 0.");
		if (nu0 <= 0)
			throw new ArgumentOutOfRangeException(nameof(nu0), "Total expected failures must be > 0.");
		if (tau < 0)
			throw new ArgumentOutOfRangeException(nameof(tau), "Execution time cannot be negative.");
	}

	public double GenMagicNum(int choice, string path, IFileReader fileReader)
	{
		ArgumentNullException.ThrowIfNull(fileReader);
		if (choice < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(choice));
		}
		string[] magicStrings = fileReader.Read(path);
		if (choice >= magicStrings.Length)
		{
			throw new ArgumentOutOfRangeException(nameof(choice));
		}
		double magicNumber = double.Parse(magicStrings[choice]);
		return 2 * Math.Abs(magicNumber);
	}
}