Shader "Custom/UnityLogoShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 vertexNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

       void vert (inout appdata_full v, out Input o) 
       {
           UNITY_INITIALIZE_OUTPUT(Input,o);
           o.vertexNormal = abs(v.normal); // Aah, this is why normal wasnt Normal!
       }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex;
            float3 normal = IN.vertexNormal;
            float c=0;

            float s1 = 0.37;
            float s2 = 0.8;

            // right panel
            if (normal.z>0.99)
            {
                c= 1-((step(uv.x,0.8)-step(uv.x,0.37))*(step(uv.y,0.63)-step(uv.y,0.2)));
                float l =1-(distance(uv.x,1-uv.y)<0.1&uv.y>0.5);

                o.Albedo = c*l;
            }else{ // other panels
                
                c = 1-((step(uv.x,s2)-step(uv.x,s1))*(step(uv.y,s2)-step(uv.y,s1))); // lol
                float l =1-(distance(uv.x,uv.y)<0.1&uv.x<0.5); // lolx2
                o.Albedo = c*l;
            }
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
 