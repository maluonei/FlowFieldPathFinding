Shader "Custom/testShader4"{
	Properties{
	  _Color("Color", Color) = (1,1,1,1)								//主颜色数值
	  _MainTex("Albedo (RGB)", 2D) = "white" {}							//2D纹理数值
	  _Shininess("Shininess ", Range(0,10)) = 10						//镜面反射系数
	  _Ambient("Ambient", range(0, 1)) = 0.01                            //环境光系数
	}
		SubShader{
		  CGPROGRAM
		  #pragma surface surf BlingPhong									//表面着色器编译指令
		  sampler2D _MainTex;										//2D纹理属性
		  fixed4 _Color;												//主颜色属性
		  float _Shininess;											//镜面反射系数属性
		  float _Ambient;
		  struct Input {
			float2 uv_MainTex;											//uv纹理坐标
		  };
		  float4 LightingBlingPhong(SurfaceOutput s, float3 lightDir,half3 viewDir, half atten) {//光照模型函数
			float4 c;
			float diffuseF = max(0,dot(s.Normal,lightDir));						//计算漫反射强度
			float specF;
			float3 H = normalize(lightDir + viewDir);						//计算视线与光线的半向量
			float specBase = max(0,dot(s.Normal,H));					//计算法线与半向量的点积
			specF = pow(specBase,_Shininess);						//计算镜面反射强度
			c.rgb = s.Albedo * _LightColor0 * diffuseF * atten + _LightColor0 * specF + _Ambient * _LightColor0;
			//结合漫反射光与镜面反射光计算最终光照颜色
			c.a = s.Alpha;
			return c;												//返回最终光照颜色
		  }
		  void surf(Input IN, inout SurfaceOutput o) {					//表面着色器函数
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;			//根据UV坐标从纹理提取颜色
			o.Albedo = c.rgb;										//设置颜色
			o.Alpha = c.a;											//设置透明度
		  }
		  ENDCG
	  }
		  FallBack "Diffuse"											//降级着色器
}
