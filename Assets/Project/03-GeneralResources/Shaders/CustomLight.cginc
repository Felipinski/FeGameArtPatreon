/*
This method calculate a simple diffuse light
*/
void diffuseLight(inout float4 color, float3 worldNormal)
{
	worldNormal = normalize(worldNormal);

	float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

	float NdotL = dot(worldNormal, lightDirection) * 0.5 + 0.5;

	color *= NdotL;
}

//This variable MUST be in the main shader properties, otherwise
//it's main value will be the default value (0 in this case)
float _ToonLightStep;

/*
This method calculate a simple toon light
*/
void toonLight(inout float4 color, float3 worldNormal)
{
	worldNormal = normalize(worldNormal);

	float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

	float NdotL = dot(worldNormal, lightDirection) * 0.5 + 0.5;

	NdotL = 1 - step(NdotL, _ToonLightStep);

	color *= NdotL;
}

