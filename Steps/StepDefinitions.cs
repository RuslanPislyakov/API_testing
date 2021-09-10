using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace API_testing.Steps
{
    [Binding]
    public sealed class StepDefinitions
    {
        private IRestResponse response;
        private ExpressionResponse deserializedResponse;
        private string number;

        [Given("the number is (.*)")]
        public void GivenTheNumber(string number)
        {
            this.number = number;
        }

        [When("I extract the square root")]
        public void ExtractTheSquareRoot()
        {
            RestClient client = new RestClient("http://api.mathjs.org/v4/");
            RestRequest request = new RestRequest("?expr=sqrt(" + number + ")", Method.GET);
            response = client.Execute(request);
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string result)
        {
            Assert.That(response.Content, Is.EqualTo(result));
        }
        
        [When("I send (.*) on API")]
        public void SendExpressionToEvaluate(string expr)
        {
            ExpressionRequest expression = new ExpressionRequest();
            expression.expr = expr;

            string body = JsonConvert.SerializeObject(expression);

            var client = new RestClient("http://api.mathjs.org/v4/");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("content-type", "application/json");
            request.AddHeader("user-agent", "Learning RestSharp");
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            deserializedResponse = JsonConvert.DeserializeObject<ExpressionResponse>(response.Content);
        }

        [Then("the result of expression should be (.*)")]
        public void ThenTheResultOfExpressionShouldBe(string result)
        {
            Assert.That(deserializedResponse.result, Is.EqualTo(result));
        }
    }
}
