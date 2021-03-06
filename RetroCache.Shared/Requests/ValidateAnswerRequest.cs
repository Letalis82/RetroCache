﻿using System;

namespace RetroCache.Shared.Requests
{
    public class ValidateAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public string GivenAnswer { get; set; }

        public ValidateAnswerRequest(Guid questionId, string givenAnswer)
        {
            QuestionId = questionId;
            GivenAnswer = givenAnswer;
        }

        public ValidateAnswerRequest()
        {

        }
    }
}
