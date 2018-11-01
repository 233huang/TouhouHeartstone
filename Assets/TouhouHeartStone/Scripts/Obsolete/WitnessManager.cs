﻿using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace TouhouHeartstone
{
    public class WitnessManager : THManager
    {
        protected void Start()
        {
            game.game.records.onWitness += onWitness;
        }
        private void onWitness(Dictionary<int, Witness> dicWitness)
        {
            Witness witness = dicWitness[game.network.id];
            witness.number = _witnessed.Count;
            add(witness);
        }
        public void add(Witness witness)
        {
            if (witness == null || witness.number < _witnessed.Count)
                return;
            if (witness.number == _witnessed.Count)
            {
                _witnessed.Add(witness);
                Debug.Log(witness, this);
                onWitnessAdded.Invoke(witness);
                if (_hungup.Count > 0)
                {
                    Witness next = _hungup.FirstOrDefault(e => { return e.number == _witnessed.Count; });
                    if (next != null)
                    {
                        _hungup.Remove(next);
                        add(next);
                    }
                }
            }
            else
                _hungup.Add(witness);
        }
        public Witness getWitness(int number)
        {
            return _witnessed[number];
        }
        public int hungupCount
        {
            get { return _hungup.Count; }
        }
        public void getMissingRange(out int min, out int max)
        {
            min = _witnessed.Count;
            max = _hungup.Min(e => { return e.number; }) - 1;
        }
        [SerializeField]
        List<Witness> _hungup = new List<Witness>();
        [SerializeField]
        List<Witness> _witnessed = new List<Witness>();
        public WitnessEvent onWitnessAdded
        {
            get { return _onWitnessAdded; }
        }
        [SerializeField]
        WitnessEvent _onWitnessAdded = new WitnessEvent();
    }
}