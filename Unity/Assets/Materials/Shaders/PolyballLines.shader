Shader "Cg shader using blending" {

  Properties {
  	uDayCoeff ("uDayCoeff", float) = 0.0
 }
 
   SubShader {
      Tags { "Queue" = "Transparent" } 
         // draw after all opaque geometry has been drawn
      Pass {
      	Cull Off
         ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects
 
         Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
 
         CGPROGRAM 
 
         #pragma vertex vert 
         #pragma fragment frag
         
         uniform float uDayCoeff;
 
         float4 vert(float4 vertexPos : POSITION) : SV_POSITION 
         {
            return mul(UNITY_MATRIX_MVP, vertexPos);
         }
 
         float4 frag(void) : COLOR 
         {
         	float dayCoeff = saturate(uDayCoeff);
         	
            return float4(1.0-dayCoeff*0.9,1.0-dayCoeff*0.9,1.0-dayCoeff*0.9, lerp(0.3, 1.0, dayCoeff)); 
               // the fourth component (alpha) is important: 
               // this is semitransparent green
         }
 
         ENDCG  
      }
   }
}