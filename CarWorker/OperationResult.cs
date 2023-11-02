﻿using System.Net;

namespace CarReservationWorkers
{
    public class OperationResult
    {
        private Error _error;
        public Error Error => _error;

        public bool HasError => Error != null;

        public OperationResult() { }

        public OperationResult(HttpStatusCode type, string error)
        {
            CreateError(type, error);
        }

        public void CreateError(HttpStatusCode type, string error)
        {
            if (_error == null)
            {
                _error = new Error
                {
                    Type = type,
                    Message = error
                };
            }
            else
            {
                _error.Type = type;
                _error.Message = error;
            }
        }
    }
    public class Error
    {
        public string Message { get; set; }
        public HttpStatusCode Type { get; set; }
    }

}
