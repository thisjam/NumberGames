using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NumberGames
{
    internal class FMS
    {
        public enum State { ready, play, fail, success,win }
        private State currentState;
        private bool IsRunning = false;
        private Dictionary<State, Action> dic = new Dictionary<State, Action>();
        //Task task;

        //CancellationTokenSource Token;
        public FMS(State _firstState = State.ready)
        {
            currentState = _firstState;
  
        }
        public void AddState(State state, Action action)
        {
            dic[state] = action;
        }
        public void start()
        {
            NextTo(currentState);
        }
        //public void stop()
        //{

        //    Token.Cancel();
        //    task.Dispose();

        //}

        public void NextTo(State newState)
        {
        
            currentState = newState;
            Trace.WriteLine(currentState);
            dic[currentState]();
        }
    }
}
