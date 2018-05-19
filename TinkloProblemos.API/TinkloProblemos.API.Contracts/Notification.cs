using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts
{
        public class Filter
        {
            public string field { get; set; }
            public string key { get; set; }
            public string relation { get; set; }
            public string value { get; set; }
        }

        public class Data
        {
            public int problem_id { get; set; }
        }

        public class Contents
        {
            public string en { get; set; }
        }

        public class Notification
        {
            public Notification(string userId, int problemId, string content)
            {
                app_id = "3c1aaa0e-b87c-4392-9b04-226e3c465c70";
                filters = new List<Filter>
                {
                    new Filter
                    {
                        field = "tag",
                        key = "user_id",
                        relation = "=",
                        value = userId
                    }
                };
                data = new Data
                {
                    problem_id = problemId
                };
                contents = new Contents
                {
                    en = content
                };
            }

            public Notification()
            {

            }
            public string app_id { get; set; }
            public List<Filter> filters { get; set; }
            public Data data { get; set; }
            public string url { get; set; }
            public Contents contents { get; set; }
        }
    }
