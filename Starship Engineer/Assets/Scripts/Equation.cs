using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equation: MonoBehaviour
{


    // It can display it's stats
    // And it's equation
    // And given input and opposing stat
    


    // How does equation work?
    // Generate two answers, and an operator.
    // Calculate answer, and form an equation.

    // Subtraction and Division are inversions of addition and multiplication, so they start with higher answers.

    // Powers/Roots? I think I'll ignore those.

    // Multi-part equations? More complex, maybe do that later after basic functionality. Its more a change to this specific class than anything.

    public enum OPERATION{Addition = 1, Subtraction = 2, Multiplication = 3, Division = 4};


    int answer = 0;
    string equation;
    int difficulty;

    // Use this for initialization
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupEquation(int currentLevel)
    {

        Equation.OPERATION Operator = (Equation.OPERATION)UnityEngine.Random.Range(1, Mathf.Min(currentLevel+1,4));

        int minOperand = 1; 
        int maxOperand = 10;

        switch (Operator)
        {
            // +
            case Equation.OPERATION.Addition:
                minOperand = Mathf.Min((currentLevel * currentLevel), 10);// 1, 4, 9, 10, 10 repeats.
                maxOperand = Mathf.Min(10 * currentLevel, 250);
                // Cap at 250
                break;

            // -
            case Equation.OPERATION.Subtraction:
                minOperand = Mathf.Min((currentLevel * currentLevel), 10);// 1, 4, 9, 10, 10 repeats.
                maxOperand = Mathf.Min(10 * (currentLevel - 1), 250);
                // Cap at 250. Grow one level behind addition.
                break;

            // *
            case Equation.OPERATION.Multiplication:
                minOperand = Mathf.Min(currentLevel-2, 3);// 1,2,3, 3 repeats 
                maxOperand = Mathf.Min(5 + currentLevel, 25);
                // Cap at 20
                break;

            // /
            case Equation.OPERATION.Division:
                minOperand = Mathf.Min(currentLevel - 3, 3);// 1,2,3, 3 repeats 
                maxOperand = Mathf.Min(4 + currentLevel, 20);
                // Cap at 20
                break;

            default:
                Debug.Log("Unexpected Operator");
                break;
        }

        int operandA = UnityEngine.Random.Range(minOperand, maxOperand + 1);
        int operandB = UnityEngine.Random.Range(minOperand, maxOperand + 1);
        difficulty = operandA + operandB;

        switch (Operator)
        {
        // +
            case OPERATION.Addition:
                answer = operandA + operandB;
                equation = operandA + " + " + operandB;
                break;

        // -
            case OPERATION.Subtraction:
            // Subtraction is a special case.
            // Inversion of Addition in effect
                answer = operandA;
                equation = (operandA+operandB) + " - " + operandB;
                break;

        // *
            case OPERATION.Multiplication:
                answer = operandA * operandB;
                equation = operandA + " X " + operandB;
                difficulty *= 2;
                break;

        // /
            case OPERATION.Division:
            // Division is a Special Case
            // Ensure even numbers by doing it as a multiplication
                answer = operandA;
                equation = (operandA*operandB) + " / " + operandB;
                difficulty *= 3;
                break;

            default:
                Debug.Log("Unexpected Operator");
                break;
        }

        if (operandA % 10 == 0 || operandB % 10 == 0 || answer % 10 == 0 || operandA==operandB)
        {
            // 10 places are much easier for normal people, and me as well. Hence, cut difficulty in said case.
            difficulty /= 2;
        }

    }

    public int GetAnswer()
    {
        return answer;
    }

    public string GetEquation()
    {
        return equation;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

}



