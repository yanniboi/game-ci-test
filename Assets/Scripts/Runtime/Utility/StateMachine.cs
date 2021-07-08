using System;
using System.Collections.Generic;
using NPC.Actions;
using UnityEngine;

namespace Utility
{
   public class StateMachine
   {
      private IState _currentState;
   
      private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type,List<Transition>>();
      private List<Transition> _currentTransitions = new List<Transition>();
      private List<Transition> _anyTransitions = new List<Transition>();
   
      private static List<Transition> EmptyTransitions = new List<Transition>(0);

   
      private bool StateHasNotChanged(IState state) => (state == _currentState);

   
      public void Tick()
      {
         var transition = GetNextTransition();
         if (transition != null)
            SetState(transition.To);
      
         _currentState?.Tick();
      }

      public void SetState(IState state)
      {
         if (StateHasNotChanged(state))
         {
            return;
         }

         Debug.Log("New state " + state);

         _currentState?.OnExit();
         _currentState = state;
         _currentState.OnEnter();
      
         SetCurrentTransitions();
      }

      private void SetCurrentTransitions()
      {
         _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
         if (_currentTransitions == null)
            _currentTransitions = EmptyTransitions;
      }


      public void AddTransition(IState from, IState to, Func<bool> predicate)
      {
         List<Transition> transitions = GetTransitionsForState(from);
         transitions.Add(new Transition(to, predicate));
      }

      private List<Transition> GetTransitionsForState(IState state)
      {
         if (_transitions.TryGetValue(state.GetType(), out var transitions) == false)
         {
            transitions = new List<Transition>();
            _transitions[state.GetType()] = transitions;
         }

         return transitions;

      }

      public void AddAnyTransition(IState state, Func<bool> predicate)
      {
         _anyTransitions.Add(new Transition(state, predicate));
      }

      private class Transition
      {
         public Func<bool> Condition { get; }
         public IState To { get; }

         public Transition(IState to, Func<bool> condition)
         {
            To = to;
            Condition = condition;
         }
      }

      private Transition GetNextTransition()
      {
         foreach(var transition in _anyTransitions)
            if (transition.Condition())
               return transition;
      
         foreach (var transition in _currentTransitions)
            if (transition.Condition())
               return transition;

         return null;
      }
   }
}