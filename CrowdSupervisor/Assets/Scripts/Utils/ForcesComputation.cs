using System.Collections;
using System.Collections.Generic;

public static class ForcesComputation
{
    public static float ConstantForce(float factor)
    {
       return factor;
    }

    public static float LinearForce(float distance, float factor, float shift = 0)
    {
        return (distance-shift) * factor;
    }

    public static float InverseLinearForce(float distance, float factor, float shift = 0)
    {
        if((distance - shift) <= 0){ return 0;}
        return factor/(distance - shift);
   }

   public static float InverseSquareForce(float distance, float factor, float shift = 0)
   {
       if((distance - shift) <= 0){ return 0;}
       return factor/((distance - shift)*(distance - shift));
   }


}
