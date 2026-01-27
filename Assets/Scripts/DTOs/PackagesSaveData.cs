using System;
using System.Collections.Generic;

namespace PetrushevskiApps.WhosGame.Scripts.DTOs
{
    [Serializable]
    public class PackagesSaveData
    {
        public List<PackageDto> Packages { get; set; } = new();

        public PackagesSaveData() { }

        public PackagesSaveData(List<PackageDto> packages) : this()
        {
            Packages = packages ?? new List<PackageDto>();
        }
    }
}
