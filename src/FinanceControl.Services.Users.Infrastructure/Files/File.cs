﻿using FinanceControl.Services.Users.Domain.Types;

namespace FinanceControl.Services.Users.Infrastructure.Files
{
    public class File : ValueObject<File>
    {
        public string Name { get; protected set; }
        public string ContentType { get; protected set; }
        public byte[] Bytes { get; protected set; }
        public long SizeBytes => Bytes.Length;

        public File()
        {
        }

        protected File(string name, string contentType, byte[] bytes)
        {
            Name = name;
            ContentType = contentType;
            Bytes = bytes;
        }

        public static File Empty => new File();

        public static File Create(string name, string contentType, byte[] bytes)
        {
            return new File(name, contentType, bytes);
        }

        protected override bool EqualsCore(File other)
        {
            return Bytes.Equals(other.Bytes);
        }

        protected override int GetHashCodeCore()
        {
            return Bytes.GetHashCode();
        }
    }
}