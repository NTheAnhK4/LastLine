
using UnityEngine;

public class MoveHandler : ActionHandler
{
   

   protected virtual void Move()
   {
      
   }
   protected void Update()
   {
      if(animHandler.currentState != AnimHandler.State.Move) return;
      Move();
   }
}
