﻿using System.IO;
using FinanceControl.Services.Users.Domain.Types;

namespace FinanceControl.Services.Users.Infrastructure.Files
{
    public class FileStreamInfo : ValueObject<FileStreamInfo>
    {
        public string Name { get; protected set; }
        public string ContentType { get; protected set; }
        public Stream Stream { get; protected set; }

        protected FileStreamInfo()
        {
        }

        protected FileStreamInfo(string name, string contentType, Stream stream)
        {
            Name = name;
            ContentType = contentType;
            Stream = stream;
        }

        public static FileStreamInfo Empty => new FileStreamInfo();

        public static FileStreamInfo Create(string name, string contentType, Stream stream)
        {
            return new FileStreamInfo(name, contentType, stream);
        }

        protected override bool EqualsCore(FileStreamInfo other)
        {
            return Stream.Equals(other.Stream);
        }

        protected override int GetHashCodeCore()
        {
            return Stream.GetHashCode();
        }
    }
}