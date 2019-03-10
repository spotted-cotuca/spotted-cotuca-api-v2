using SpottedCotuca.Application.Entities.Models;
using System;

namespace SpottedCotuca.Application.Tests.TestUtils.Builders
{
    public class SpotBuilder
    {
        private long _id = Generate.NewId();
        private string _message = Generate.NewMessage();
        private Status _status = Status.Pending;
        private DateTime _postDate = DateTime.Now;
        private long _facebookId = Generate.NewId();
        private long _twitterId = Generate.NewId();

        public SpotBuilder() { }

        public Spot Build()
        {
            return new Spot
            {
                Id = _id,
                Message = _message,
                Status = _status,
                PostDate = _postDate,
                FacebookId = _facebookId,
                TwitterId = _twitterId
            };
        }

        public SpotBuilder WithId(long id)
        {
            _id = id;
            return this;
        }

        public SpotBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public SpotBuilder WithStatus(Status status)
        {
            _status = status;
            return this;
        }

        public SpotBuilder WithPostDate(DateTime postDate)
        {
            _postDate = postDate;
            return this;
        }

        public SpotBuilder WithFacebookId(long facebookId)
        {
            _facebookId = facebookId;
            return this;
        }

        public SpotBuilder WithTwitterId(long twitterId)
        {
            _twitterId = twitterId;
            return this;
        }
    }
}
