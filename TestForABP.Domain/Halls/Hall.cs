using System;
using System.Collections.Generic;
using System.Text;
using TestForABP.Domain.Common;
using TestForABP.Domain.Halls.VO;

namespace TestForABP.Domain.Halls
{
    public class Hall : Entity , IAggregateRoot
    {
        public Guid Id { get;private set; }
        public string Name { get; private set; }
        public Capacity Capacity { get; private set; }
        public Money BaseHourlyRate { get; private set; }


        private readonly List<Amenity> _amenities = new();
        public IReadOnlyCollection<Amenity> Amenities => _amenities.AsReadOnly();

        private Hall () { }

        private Hall(Guid id , string name , Capacity capacity , Money baseHourlyRate)
        {
            Id = id; 
            Name = name;
            Capacity = capacity;
            BaseHourlyRate = baseHourlyRate;
        }

        public static Hall Create (string name , Capacity capacity , Money baseHourlyRate)
        {
            EnsureValidName(name);

            return new Hall (Guid.NewGuid() , name , capacity , baseHourlyRate);
        }

        public void UpdateDetails(string name, Capacity capacity, Money baseHourlyRate)
        {
            EnsureValidName(name);

            this.Name = name;
            this.Capacity = capacity;
            this.BaseHourlyRate = baseHourlyRate;
        }

        public void AddAmenity(string name , Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва послуги не може бути порожньою.");

            var amenity = new Amenity(Guid.NewGuid() , name , price);
            

            if (_amenities.Any(a => a.Id == amenity.Id || a.Name.Equals(amenity.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Послуга '{amenity.Name}' вже існує в цьому залі.");
            }

            _amenities.Add(amenity);
        }

        public void RemoveAmenity(Guid amenityId)
        {
            var amenity = _amenities.FirstOrDefault(a => a.Id == amenityId);
            if (amenity is null)
            {
                throw new ArgumentException("Послугу не знайдено в даному залі.");
            }

            _amenities.Remove(amenity);
        }

        private static void EnsureValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Назва залу не може бути порожньою.");
            }
        }

    }
}
