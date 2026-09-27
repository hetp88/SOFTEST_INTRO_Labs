@BasicMusa
Feature: UsingCalculatorBasicReliability
  In order to calculate the Basic Musa model's failures and intensities
  As a Software Quality Metric enthusiast
  I want to use my calculator to do this

  Scenario: Current failure intensity at zero execution time
    Given I have a calculator
    And the Basic Musa parameters are
      | Lambda0 | Nu0 | Tau |
      | 10      | 100 | 0   |
    When I calculate the current failure intensity
    Then the result should be 10

  Scenario: Current failure intensity after some execution time
    Given I have a calculator
    And the Basic Musa parameters are
      | Lambda0 | Nu0 | Tau |
      | 10      | 100 | 10  |
    When I calculate the current failure intensity
    Then the result should be 3.678794412

  Scenario: Expected cumulative failures at zero execution time
    Given I have a calculator
    And the Basic Musa parameters are
      | Lambda0 | Nu0 | Tau |
      | 10      | 100 | 0   |
    When I calculate the expected cumulative failures
    Then the result should be 0

  Scenario: Expected cumulative failures after some execution time
    Given I have a calculator
    And the Basic Musa parameters are
      | Lambda0 | Nu0 | Tau |
      | 10      | 100 | 10  |
    When I calculate the expected cumulative failures
    Then the result should be 63.212055883

  Scenario Outline: Reject invalid Basic Musa parameters
    Given I have a calculator
    And the Basic Musa parameters are
      | Lambda0   | Nu0   | Tau   |
      | <lambda0> | <nu0> | <tau> |
    When I calculate the current failure intensity
    Then the calculation should be rejected

    Examples:
      | lambda0 | nu0 | tau |
      | 0       | 100 | 10  |
      | 10      | 0   | 10  |
      | 10      | 100 | -1  |