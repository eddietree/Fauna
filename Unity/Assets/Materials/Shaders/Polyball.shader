 Shader "Custom/Polyball" {
 
  Properties {
  	uDayCoeff ("uDayCoeff", float) = 0.0
 }
        SubShader {
            Pass {
            	Cull Off

    CGPROGRAM
    
     

    #pragma vertex vert
    #pragma fragment frag
    
     uniform float uDayCoeff;
    
    // vertex input
    struct appdata {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        fixed4 color : COLOR;
        float4 texcoord : TEXCOORD0;
    };

	// VS->PS inputs
    struct v2f {
        float4 pos : SV_POSITION;
        float3 color : COLOR0;
        float3 worldPos : TEXCOORD0;
        float2 uv : TEXCOORD1;
        float3 normal : TEXCOORD2;
    };

	// vertex shader
    v2f vert (appdata v)
    {
    	float3 worldPos = v.vertex;
    	
        v2f o;
        o.pos = mul (UNITY_MATRIX_MVP, float4(worldPos,1.0));
        o.color = v.color;
        o.uv = v.texcoord.xy;
        o.normal = v.normal;
        o.worldPos = worldPos;
        
        return o;
    }

	// fragment shader
    float4 frag (v2f i) : COLOR
    {
    	if ( i.uv.x < 0.1 )
    	{
    		clip( sin(i.worldPos.y * 80.0 ) );
    		return float4 (1,1,1, 1);
    	}
    		
    	//clip( sin(i.worldPos.y * 20.0) );
    	//return float4( i.uv.xy, 0.0, 1.0);
    	float3 finalColor = i.color;
    	float3 finalColorInv = float3(1.0,1.0,1.0)-finalColor;
    	
    	finalColor = finalColor + ( finalColorInv-finalColor ) * saturate(uDayCoeff) * 0.9;
    	
        return float4 (finalColor, 1);
    }
    ENDCG

            }
        }
        Fallback "VertexLit"
    }
