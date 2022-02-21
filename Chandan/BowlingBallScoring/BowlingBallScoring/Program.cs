using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingBallScoring
{
    class Program
    {

        /* Logic: The score for the frame is the total number of pins knocked down, plus bonuses for strikes and spares.*/

        /* Spare -> 10 pins down in 2 tries. 
         * Bonus = number of pins knocked down by the next roll.  */

        /* Strike -> 10 pins down in 1 try.
         * Bonus = next two balls rolled. */

        static void Main(string[] args)
        {
            int frameScore = 0, prevFrame = 0, prevFrameTwo = 0, bowlOne, bowlTwo = 0, totalScore = 0, extraFrame;
            bool strike = false, strikeTwo = false, spare = false;
            String score1 = "", score2 = "", LastFrameTwo = "", LastFrameThree = "", frameNum = "", lineToDecor = "";


            Console.WriteLine("Bowling ball scoring");

            // The game consists of 10 frames.
            for (int frameCnt = 1; frameCnt <= 10; frameCnt++)
            {
                Console.WriteLine("Please Enter your Scores for Frame {0}:", frameCnt);
                do //bowlOne loop
                {
                    Console.Write("Bowl 1:");
                    bowlOne = int.Parse(Console.ReadLine());
                } while (bowlOne > 10 || bowlOne < 0); //checks for valid bowlOne input


                /* A spare is when the player knocks down all 10 pins in two tries. 
                * The bonus for that frame is the number of pins knocked down by the next roll. */
                if (spare == true)// if previous frame was a spare add in the extra points now
                {
                    prevFrame = 10 + bowlOne;
                    spare = false;
                    totalScore = prevFrame + totalScore;
                    score2 = scoreTwo(totalScore, score2);

                }
                if (strikeTwo == true && bowlOne == 10)
                {
                    prevFrameTwo = 30;
                    totalScore = prevFrameTwo + totalScore;
                    score2 = scoreTwo(totalScore, score2);
                }
                if (strikeTwo == true && bowlOne != 10)
                {
                    strikeTwo = false;
                    prevFrameTwo = 10 + 10 + bowlOne;
                    totalScore = prevFrameTwo + totalScore;
                    score2 = scoreTwo(totalScore, score2);
                }
                if (strike == true && bowlOne == 10)
                {
                    strikeTwo = true;
                    prevFrameTwo = 20;
                }

                if (bowlOne < 10) //check to make sure there was not a strike on first bowl
                {
                    do //bowlTwo loop
                    {
                        Console.Write("Bowl 2:");
                        bowlTwo = int.Parse(Console.ReadLine());
                    } while (bowlTwo > (10 - bowlOne) || bowlTwo < 0);

                    if (bowlOne + bowlTwo == 10) // Sapre req 10 pins down in 2 tries.
                    {
                        spare = true;
                        score1 += bowlOne + "-/ | "; 
                    }

                    if (strikeTwo == true && frameCnt == 10)
                    {
                        prevFrameTwo = 10 + 10 + bowlTwo;
                        totalScore = prevFrameTwo + totalScore;
                        score2 = scoreTwo(totalScore, score2);
                        strikeTwo = false;
                    }

                    if (strike == true && bowlOne != 10)
                    {
                        strike = false;
                        prevFrame = 10 + bowlOne + bowlTwo;
                        totalScore = totalScore + prevFrame;
                        score2 = scoreTwo(totalScore, score2);
                    }
                    if (spare != true && strike != true && strikeTwo != true)
                    {
                        frameScore = bowlOne + bowlTwo;
                        totalScore = totalScore + frameScore;
                        score2 = scoreTwo(totalScore, score2);
                        if (frameCnt != 10)
                            score1 += " " + bowlOne + "-" + bowlTwo + " |";
                        else
                            score1 += " " + bowlOne + "-" + bowlTwo;
                    }
                }
                else
                {
                    strike = true;
                    prevFrame = 10;
                    if (frameCnt != 10)
                        score1 += " X-  |";
                }
                if (frameCnt == 10 && strike == true)
                {
                    do
                        bowlTwo = int.Parse(Console.ReadLine());
                    while (bowlTwo < 0 || bowlTwo > 10);

                    if (strikeTwo == true)
                    {
                        prevFrameTwo = 10 + 10 + bowlTwo;
                        totalScore = prevFrameTwo + totalScore;
                        score2 = scoreTwo(totalScore, score2);
                        strikeTwo = false;
                    }
                }

                if (frameCnt == 10 && (spare == true || strike == true))
                {
                    do
                        extraFrame = int.Parse(Console.ReadLine());
                    while (extraFrame < 0 || extraFrame > 10);
                    if (strike == true)
                    {
                        prevFrame = 10 + bowlTwo + extraFrame;
                        totalScore = totalScore + prevFrame;
                        score2 = scoreTwo(totalScore, score2);
                        if (bowlTwo == 10)
                            LastFrameTwo = "-X";
                        else
                            LastFrameTwo += bowlTwo;
                        if (extraFrame == 10)
                            LastFrameThree = "-X";
                        else
                            LastFrameThree += extraFrame;
                        score1 += " X" + LastFrameTwo + LastFrameThree;
                    }
                    else
                    {
                        if (extraFrame == 10)
                            LastFrameThree = "-X";
                        else
                            LastFrameThree += extraFrame;
                        if (bowlTwo + extraFrame == 10 && extraFrame != 10)
                            LastFrameThree = "-/";
                        else
                            LastFrameThree += extraFrame;
                        totalScore = totalScore + 10 + extraFrame;
                        score2 = scoreTwo(totalScore, score2);
                        score1 += bowlOne + "-/" + LastFrameThree;
                    }
                }
                frameNum += frameCnt + "    ";
                lineToDecor += "------";
            }
            Console.WriteLine(frameNum);
            Console.WriteLine(lineToDecor);
            Console.WriteLine(score1);
            Console.WriteLine(score2);
            Console.ReadLine();

        }


        //For indentation of result score table.        
        static String scoreTwo(int totalScore, String score2)
        {
            if (calculateIntLength(totalScore) == 1)
                score2 += totalScore + "     ";
            else if (calculateIntLength(totalScore) == 2)
                score2 += totalScore + "    ";
            if (calculateIntLength(totalScore) == 3)
                score2 += totalScore + "   ";
            return score2;
        }

        // To calculate integer length of input for indentation.
        static int calculateIntLength(int integer)
        {
            int length = 1;
            if (integer / 10 > 0)
                length = 2;
            if (integer / 100 > 0)
                length = 3;
            return length;
        }

       
    }
}


