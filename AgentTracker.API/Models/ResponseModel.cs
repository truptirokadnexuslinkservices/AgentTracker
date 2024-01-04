using static AgentTracker.API.Models.Responses;

namespace AgentTracker.API.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel() { }



        public ResponseModel(T resp)
        {
            this.ResponseCode = ResponseCode.Success;
            this.ResponseObject = resp;
        }

        public ResponseModel(T resp, ResponseCode respCode, string respMsg = null)
        {
            this.ResponseCode = respCode;
            this.ResponseObject = resp;
            if (respMsg == null)
            {
                this.ResponseMessage = ResponseMessages[(int)respCode];
            }
            else
            {
                this.ResponseMessage = respMsg;
            }
        }

        public ResponseModel(T resp, int respCode, string respMsg = null)
        {
            this.ResponseCode = (ResponseCode)respCode;
            this.ResponseObject = resp;
            if (respMsg == null)
            {
                this.ResponseMessage = ResponseMessages[respCode];
            }
            else
            {
                this.ResponseMessage = respMsg;
            }
        }

        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T ResponseObject { get; set; }
         
    }
}
