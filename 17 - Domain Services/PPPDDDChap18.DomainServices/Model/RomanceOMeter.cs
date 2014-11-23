using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices
{
    // Domain Service - part of Ubiquitous Language
    public class RomanceOMeter
    {
        // stateless - collaborators are allowed, though

        // behavior-only
        public CompatibilityRating AssessCompatibility(LoveSeeker seeker1, LoveSeeker seeker2)
        {
            var rating = new CompatibilityRating();

            // orchestrate Entities:
            // compare dating history, blood type, lifestyle etc
            if (seeker1.BloodType == seeker2.BloodType)
            {
                rating = rating.Boost(CompatibilityRating(250));
            }

            // ..

            // return another Domain Object (Value Object in this case)
            return rating;
        }

        private CompatibilityRating CompatibilityRating(int value)
        {
            // ...
            return new CompatibilityRating();
        }
    }


    // entity
    public class LoveSeeker
    {
        public Guid Id { get; protected set; }

        public BloodType BloodType { get; private set; }

        // ...
    }

    // Value Objects

    public class CompatibilityRating
    {
        public CompatibilityRating Boost(CompatibilityRating rating)
        {
            // ..
            return null;
        }

        // ..
    }

    public class BloodType
    {
        // ...
    }
}
