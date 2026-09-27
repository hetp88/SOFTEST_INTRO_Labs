@Factorial
Feature: UsingCalculatorFactorial
  In order to count arrangements
  As a calculator user
  I want to be told the factorial of a whole number

  Scenario: Factorial of a normal number
    Given I have a calculator
    When I calculate the factorial of 5
    Then the factorial result should be 120

  Scenario: Factorial of zero
    Given I have a calculator
    When I calculate the factorial of 0
    Then the factorial result should be 1

  Scenario: Reject an unsupported value
    Given I have a calculator
    When I calculate the factorial of 21
    Then the factorial request should be rejected