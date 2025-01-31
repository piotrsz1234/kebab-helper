﻿using Smile;
using WebApp.Enums;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Wrappers
{
    public class ModelWrapper : IDisposable
    {
        private const string MeatNodeId = "mięso";
        private const string PriceNodeId = "cena";
        private const string VegetableNodeId = "warzywa";
        private const string SosNodeId = "sos";
        private const string RestaurantNodeId = "lokal";
        private const string ResultNodeId = "opinia";

        private readonly object _lock = new();

        private Network? _network;

        public void LoadModel()
        {
            var network = new Network();
            network.ReadFile(Path.Combine(Environment.CurrentDirectory, "DecisionModel\\model.xdsl"));

            lock (_lock)
            {
                _network = network;
            }
        }

        public void SetMeatType(MeatType type)
        {
            if (_network == null)
                throw new InvalidOperationException("Network not loaded");

            if (type == MeatType.Unknown)
            {
                _network.ClearEvidence(MeatNodeId);
                return;
            }

            _network.SetEvidence(MeatNodeId, EnumHelper.GetDescription(type));
        }

        public void SetVegetableType(VegetableType type)
        {
            if (_network == null)
                throw new InvalidOperationException("Network not loaded");

            if (type == VegetableType.Unknown)
            {
                _network.ClearEvidence(VegetableNodeId);
                return;
            }

            _network.SetEvidence(VegetableNodeId, EnumHelper.GetDescription(type));
        }

        public void SetSosType(SosType type)
        {
            if (_network == null)
                throw new InvalidOperationException("Network not loaded");

            if (type == SosType.Unknown)
            {
                _network.ClearEvidence(SosNodeId);
                return;
            }

            _network.SetEvidence(SosNodeId, EnumHelper.GetDescription(type));
        }

        public void SetPriceType(PriceType type)
        {
            if (_network == null)
                throw new InvalidOperationException("Network not loaded");

            if (type == PriceType.Unknown)
            {
                _network.ClearEvidence(PriceNodeId);
                return;
            }

            _network.SetEvidence(PriceNodeId, EnumHelper.GetDescription(type));
        }

        public List<ModelResult> GetResults()
        {
            if (_network is null)
                throw new InvalidOperationException("Network not loaded");

            _network.UpdateBeliefs();

            var mtx = _network.GetNodeValue(ResultNodeId);
            var outcomes = _network.GetOutcomeIds(RestaurantNodeId);

            var results = new List<ModelResult>();

            int dimensions = mtx.Length / outcomes.Length;
            double[] values = new double[dimensions];
            for (int i = 0; i < outcomes.Length; i++)
            {
                var parent = outcomes[i];
                
                for (int j = 0; j < dimensions; j++)
                {
                    values[j] = mtx[i + j * outcomes.Length];
                }
                
                results.Add(new ModelResult(parent, values.Max()));
            }

            return results;
        }

        static void IndexToCoords(int index, int[] dimSizes, int[] coords)
        {
            int prod = 1;
            for (int i = dimSizes.Length - 1; i >= 0; i--)
            {
                coords[i] = (index / prod) % dimSizes[i];
                prod *= dimSizes[i];
            }
        }

        public void Dispose()
        {
            _network?.Dispose();
        }
    }
}