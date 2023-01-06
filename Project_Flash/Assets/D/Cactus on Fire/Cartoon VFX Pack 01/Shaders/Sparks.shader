// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33777,y:32833,varname:node_4013,prsc:2|emission-8816-OUT;n:type:ShaderForge.SFN_Tex2d,id:7925,x:31756,y:32982,ptovrint:False,ptlb:Sparks,ptin:_Sparks,varname:node_7925,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:defe7b026b4eb8c458c842c0211b00a6,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:6197,x:31763,y:33486,varname:node_6197,prsc:2|IN-7925-G;n:type:ShaderForge.SFN_Blend,id:2171,x:31995,y:33416,varname:node_2171,prsc:2,blmd:1,clmp:True|SRC-7925-G,DST-6197-OUT;n:type:ShaderForge.SFN_RemapRange,id:2799,x:32168,y:33416,varname:node_2799,prsc:2,frmn:0,frmx:1,tomn:0,tomx:2|IN-2171-OUT;n:type:ShaderForge.SFN_Frac,id:844,x:32161,y:32929,varname:node_844,prsc:2|IN-6125-OUT;n:type:ShaderForge.SFN_Add,id:6125,x:31988,y:32929,varname:node_6125,prsc:2|A-7925-R,B-7013-OUT,C-3921-G;n:type:ShaderForge.SFN_Time,id:1901,x:31400,y:32762,varname:node_1901,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2930,x:32861,y:33025,varname:node_2930,prsc:2|A-7297-OUT,B-9059-OUT,C-7925-B;n:type:ShaderForge.SFN_Add,id:6803,x:31988,y:32788,varname:node_6803,prsc:2|A-6125-OUT,B-1992-OUT;n:type:ShaderForge.SFN_Frac,id:7679,x:32161,y:32788,varname:node_7679,prsc:2|IN-6803-OUT;n:type:ShaderForge.SFN_Blend,id:7005,x:32353,y:32848,varname:node_7005,prsc:2,blmd:17,clmp:True|SRC-7679-OUT,DST-844-OUT;n:type:ShaderForge.SFN_Multiply,id:7013,x:31756,y:32827,varname:node_7013,prsc:2|A-1901-TSL,B-3423-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3423,x:31756,y:32769,ptovrint:False,ptlb:Spark Speed,ptin:_SparkSpeed,varname:node_3423,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Blend,id:7297,x:32586,y:32948,varname:node_7297,prsc:2,blmd:6,clmp:True|SRC-7005-OUT,DST-1425-OUT;n:type:ShaderForge.SFN_RemapRange,id:7611,x:32161,y:33188,varname:node_7611,prsc:2,frmn:0,frmx:1,tomn:1,tomx:2|IN-3111-OUT;n:type:ShaderForge.SFN_Clamp01,id:3111,x:32161,y:33062,varname:node_3111,prsc:2|IN-844-OUT;n:type:ShaderForge.SFN_Lerp,id:3086,x:32379,y:33121,varname:node_3086,prsc:2|A-7611-OUT,B-3111-OUT,T-4546-OUT;n:type:ShaderForge.SFN_Slider,id:4546,x:31606,y:33420,ptovrint:False,ptlb:Trail Length,ptin:_TrailLength,varname:node_4546,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Clamp01,id:292,x:32586,y:33121,varname:node_292,prsc:2|IN-3086-OUT;n:type:ShaderForge.SFN_OneMinus,id:1104,x:32586,y:33244,varname:node_1104,prsc:2|IN-292-OUT;n:type:ShaderForge.SFN_Multiply,id:1425,x:32775,y:33244,varname:node_1425,prsc:2|A-1104-OUT,B-7165-OUT,C-3671-OUT;n:type:ShaderForge.SFN_Slider,id:7165,x:32586,y:33409,ptovrint:False,ptlb:Trail Opacity,ptin:_TrailOpacity,varname:node_7165,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_OneMinus,id:7305,x:32586,y:33528,varname:node_7305,prsc:2|IN-4546-OUT;n:type:ShaderForge.SFN_Multiply,id:3671,x:32775,y:33528,varname:node_3671,prsc:2|A-1480-OUT,B-8684-OUT;n:type:ShaderForge.SFN_Vector1,id:8684,x:32775,y:33692,varname:node_8684,prsc:2,v1:10;n:type:ShaderForge.SFN_Blend,id:9059,x:32342,y:33416,varname:node_9059,prsc:2,blmd:10,clmp:True|SRC-2799-OUT,DST-2799-OUT;n:type:ShaderForge.SFN_Clamp01,id:2313,x:33034,y:33025,varname:node_2313,prsc:2|IN-2930-OUT;n:type:ShaderForge.SFN_Multiply,id:8355,x:33260,y:32946,varname:node_8355,prsc:2|A-3001-RGB,B-2313-OUT,C-3498-OUT,D-2497-R;n:type:ShaderForge.SFN_Color,id:3001,x:33034,y:32878,ptovrint:False,ptlb:Spark Color,ptin:_SparkColor,varname:node_3001,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Blend,id:8816,x:33438,y:32946,varname:node_8816,prsc:2,blmd:10,clmp:True|SRC-8355-OUT,DST-8355-OUT;n:type:ShaderForge.SFN_Vector1,id:3498,x:33260,y:33082,varname:node_3498,prsc:2,v1:3;n:type:ShaderForge.SFN_Slider,id:481,x:31599,y:32627,ptovrint:False,ptlb:Spark Size,ptin:_SparkSize,varname:node_481,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.01,max:1;n:type:ShaderForge.SFN_VertexColor,id:3921,x:31400,y:32970,varname:node_3921,prsc:2;n:type:ShaderForge.SFN_RemapRange,id:1480,x:32586,y:33668,varname:node_1480,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-7305-OUT;n:type:ShaderForge.SFN_RemapRange,id:1992,x:31988,y:32627,varname:node_1992,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.1|IN-481-OUT;n:type:ShaderForge.SFN_VertexColor,id:2497,x:33034,y:33163,varname:node_2497,prsc:2;proporder:3001-7925-3423-4546-7165-481;pass:END;sub:END;*/

Shader "Shader Forge/Sparks" {
    Properties {
        _SparkColor ("Spark Color", Color) = (0.5,0.5,0.5,1)
        _Sparks ("Sparks", 2D) = "white" {}
        _SparkSpeed ("Spark Speed", Float ) = 0
        _TrailLength ("Trail Length", Range(0, 1)) = 1
        _TrailOpacity ("Trail Opacity", Range(0, 1)) = 1
        _SparkSize ("Spark Size", Range(0, 1)) = 0.01
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _Sparks; uniform float4 _Sparks_ST;
            uniform float _SparkSpeed;
            uniform float _TrailLength;
            uniform float _TrailOpacity;
            uniform float4 _SparkColor;
            uniform float _SparkSize;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _Sparks_var = tex2D(_Sparks,TRANSFORM_TEX(i.uv0, _Sparks));
                float4 node_1901 = _Time;
                float node_6125 = (_Sparks_var.r+(node_1901.r*_SparkSpeed)+i.vertexColor.g);
                float node_844 = frac(node_6125);
                float node_3111 = saturate(node_844);
                float node_2799 = (saturate((_Sparks_var.g*(1.0 - _Sparks_var.g)))*2.0+0.0);
                float3 node_8355 = (_SparkColor.rgb*saturate((saturate((1.0-(1.0-saturate(abs(frac((node_6125+(_SparkSize*0.1+0.0)))-node_844)))*(1.0-((1.0 - saturate(lerp((node_3111*1.0+1.0),node_3111,_TrailLength)))*_TrailOpacity*(((1.0 - _TrailLength)*0.5+0.5)*10.0)))))*saturate(( node_2799 > 0.5 ? (1.0-(1.0-2.0*(node_2799-0.5))*(1.0-node_2799)) : (2.0*node_2799*node_2799) ))*_Sparks_var.b))*3.0*i.vertexColor.r);
                float3 emissive = saturate(( node_8355 > 0.5 ? (1.0-(1.0-2.0*(node_8355-0.5))*(1.0-node_8355)) : (2.0*node_8355*node_8355) ));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
