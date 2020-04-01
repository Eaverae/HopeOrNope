using HopeNope.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HopeNope.Handlers
{
	public class FaceHandler  
	{

		// replace <myresourcename> with the string found in your endpoint URL
		const string uriBase = ApplicationConstants.FaceApiEndpoint + "/face/v1.0/detect";

		// Gets the analysis of the specified image by using the Face REST API.
		public static async Task<string> MakeAnalysisRequestAsync(byte[] image)
		{
			if (image == null)
				throw new ArgumentNullException(nameof(image));

			string contentString = string.Empty;

			HttpClient client = new HttpClient();

			// Request headers.
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApplicationConstants.FaceApiKey);

			// Request parameters. A third optional parameter is "details".
			string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
				"&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
				"emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

			// Assemble the URI for the REST API Call.
			string uri = uriBase + "?" + requestParameters;

			HttpResponseMessage response;

			// Request body. Posts a locally stored JPEG image.
			//byte[] byteData = GetImageAsByteArray(imageFilePath);

			using (ByteArrayContent content = new ByteArrayContent(image))
			{
				// This example uses content type "application/octet-stream".
				// The other content types you can use are "application/json"
				// and "multipart/form-data".
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

				// Execute the REST API call.
				response = await client.PostAsync(uri, content);

				// Get the JSON response.
				contentString = await response.Content.ReadAsStringAsync();
			}

			return contentString;
		}
	}
}
