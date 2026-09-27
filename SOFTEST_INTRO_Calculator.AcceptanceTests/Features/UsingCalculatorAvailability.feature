@Availability
Feature: UsingCalculatorAvailability
  In order to calculate MTBF and Availability
  As someone who struggles with maths
  I want to be able to use my calculator to do this

  Scenario: Calculating MTBF
    Given I have a calculator
    When I have entered 100 and 4 into the calculator and press MTBF
    Then the result should be 25

  Scenario: Calculating Availability
    Given I have a calculator
    When I have entered 80 and 20 into the calculator and press Availability
    Then the result should be 0.8

  Scenario: Reject MTBF with zero failures
    Given I have a calculator
    When I have entered 100 and 0 into the calculator and press MTBF
    Then the calculation should be rejected

  Scenario: Calculating Availability from named reliability values
    Given I have a calculator
    And the reliability values are
      | MTBF | MTTR |
      | 90   | 10   |
    When I calculate Availability from these values
    Then the result should be 0.9