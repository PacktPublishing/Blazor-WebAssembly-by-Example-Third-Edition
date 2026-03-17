using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace GuessTheNumber.Shared
{
     public partial class Game
    {
        [Inject] ILogger<Game>? logger { get; set; }
        [Parameter] public int? Digits { get; set; }
        private int digitCount = 4;
        private string answer = "";
        private string guess = "";
        private List<Row> guesses = new();
        private bool winner = false;

        protected override void OnParametersSet() {
            if (Digits.HasValue) { digitCount = (int)Digits; };
            CalculateAnswer();
        }
        private void CalculateAnswer() {
            StringBuilder calculateAnswer = new StringBuilder();
            for (int i = 0; i < digitCount; i++)
            {
                int nextDigit = new Random().Next(0, 10);
                calculateAnswer.Append(nextDigit);
            }
            answer = calculateAnswer.ToString();

            logger.LogInformation($"The answer is {answer}");
        }
        private void GuessAnswer() {
            var curGuess = new Row()
            {
                Guess = guess,
                Matches = new string[digitCount]
            };

            for (int i = 0; i < digitCount; i++)
            {
                if (answer[i] == guess[i])
                {
                    curGuess.Matches[i] = "match-pos";
                }
                else
                {
                    if (answer.Contains(guess[i]))
                    {
                        curGuess.Matches[i] = "match-val";
                    }
                }
            }
            guesses.Add(curGuess);
            
            if (guess == answer) winner = true;

            logger.LogInformation(JsonSerializer.Serialize(guesses));
            guess = "";
        }
        private void PlayAgain() {
            winner = false;
            guesses = new();
            CalculateAnswer();
        }

        public class Row
        {
            public string Guess { get; set; }
            public string[] Matches { get; set; }
        }

        private void RestartGame(ChangeEventArgs e)
        {
            digitCount = Convert.ToInt16(e.Value);
            PlayAgain();
        }

    }
}
