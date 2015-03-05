using UnityEngine;
using System.Collections;

public class ProceduralGeneration : MonoBehaviour {
	public static ProceduralGeneration instance;

	//			int
	// const
	const int ID = 0;
	const int WIDTH = 1;
	const int ARRAY = 40;

	// private
	private int bottomIndex = 0;
	private int topIndex = 0;

	private int topWidth;
	private int topHeight;
	private int bottomWidth;
	private int bottomHeight;

	private int[,] rowOneTop;
	private int[,] rowTwoTop;
	private int[,] rowThreeTop;
	private int[,] rowFourTop;

	private int[,] rowOneBottom;
	private int[,] rowTwoBottom;
	private int[,] rowThreeBottom;
	private int[,] rowFourBottom;

	private int[] topEdges;
	private int[] bottomEdges;

	private int[] topEdgesColor;
	private int[] bottomEdgesColor;

	private int tempSizeTop;
	private int tempSizeBottom;

	private int tempSizeTopRemove;
	private int tempSizeBottomRemove;

	private int previousTopIndex;
	private int previousBottomIndex;


	// 			string
	// private
	private string top = "";
	private string bottom = "";

	// Use this for initialization
	void Awake () {

		instance = this;

		previousTopIndex = 0;
		previousBottomIndex = 0;

		rowOneTop = new int[ARRAY,2];
		rowTwoTop = new int[ARRAY,2];
		rowThreeTop = new int[ARRAY,2];
		rowFourTop = new int[ARRAY,2];

		rowOneBottom = new int[ARRAY,2];
		rowTwoBottom = new int[ARRAY,2];
		rowThreeBottom = new int[ARRAY,2];
		rowFourBottom = new int[ARRAY,2];

		topEdges = new int[ARRAY];
		bottomEdges = new int[ARRAY];

		for (int i = 0; i < ARRAY; i += 1) {
			rowOneTop[i,ID] = 0;
			rowTwoTop[i,ID] = 0;
			rowThreeTop[i,ID] = 0;
			rowFourTop[i,ID] = 0;

			rowOneTop[i,WIDTH] = 0;
			rowTwoTop[i,WIDTH] = 0;
			rowThreeTop[i,WIDTH] = 0;
			rowFourTop[i,WIDTH] = 0;

			rowOneBottom[i,ID] = 0;
			rowTwoBottom[i,ID] = 0;
			rowThreeBottom[i,ID] = 0;
			rowFourBottom[i,ID] = 0;

			rowOneBottom[i,WIDTH] = 0;
			rowTwoBottom[i,WIDTH] = 0;
			rowThreeBottom[i,WIDTH] = 0;
			rowFourBottom[i,WIDTH] = 0;

			topEdges[i] = 0;
			bottomEdges[i] = 0;
		}
	}

	public Vector2 NewTopPiece(){

		topWidth = Random.Range (0, 3);
		topHeight = Random.Range (0, 4);
	
		return new Vector2(topWidth, topHeight);
	}

	public Vector2 NewBottomPiece(){

		bottomWidth = Random.Range (0, 3);
		bottomHeight = Random.Range (0, 4);
		
		return new Vector2(bottomWidth, bottomHeight);
	}


	public void AddTopPiece(int id, int height, int width){
		tempSizeTop = topIndex + width;

		/*
		print ("------------------------------------------------------------------");

		print ("ADD TOP");
		print ("id: " + id);
		print ("height: " + height);
		print ("width: " + width);
		print ("previousTopIndex: " + previousTopIndex);
		print ("topIndex: " + topIndex);
		print ("tempSizeTop " + tempSizeTop);

		*/

		for(int i = topIndex; i < tempSizeTop; i += 1){
			topEdges[i] = id;
		}

		switch (height) {
			case 1:

				for(int i = topIndex; i < tempSizeTop; i += 1){
					rowOneTop[i,ID] = id;
					rowOneTop[i,WIDTH] = width;
				}
				break;

			case 2:

				for(int i = topIndex; i < tempSizeTop; i += 1){
					rowOneTop[i,ID] = id;
					rowOneTop[i,WIDTH] = width;
					rowTwoTop[i,ID] = id;
					rowTwoTop[i,WIDTH] = width;
				}
				break;

			case 3:

				for(int i = topIndex; i < tempSizeTop; i += 1){
					rowOneTop[i,ID] = id;
					rowOneTop[i,WIDTH] = width;
					rowTwoTop[i,ID] = id;
					rowTwoTop[i,WIDTH] = width;
					rowThreeTop[i,ID] = id;
					rowThreeTop[i,WIDTH] = width;
				}
				break;

			case 4:

				for(int i = topIndex; i < tempSizeTop; i += 1){
					rowOneTop[i,ID] = id;
					rowOneTop[i,WIDTH] = width;
					rowTwoTop[i,ID] = id;
					rowTwoTop[i,WIDTH] = width;
					rowThreeTop[i,ID] = id;
					rowThreeTop[i,WIDTH] = width;
					rowFourTop[i,ID] = id;
					rowFourTop[i,WIDTH] = width;
				}
				break;
		}

		topIndex = tempSizeTop;

		//PrintTOPState ();
	}

	public void AddBottomPiece(int id, int height, int width){
		tempSizeBottom = bottomIndex + width;

		/*
		print ("------------------------------------------------------------------");

		print ("ADD BOTTOM");
		print ("id: " + id);
		print ("height: " + height);
		print ("width: " + width);
		print ("previousBottomIndex: " + previousBottomIndex);
		print ("bottomIndex: " + bottomIndex);
		print ("tempSizeBottom " + tempSizeBottom);

		*/

		for(int i = bottomIndex; i < tempSizeBottom; i += 1){
			bottomEdges[i] = id;
		}

		switch (height) {
			case 1:
				
				for(int i = bottomIndex; i < tempSizeBottom; i += 1){
					rowOneBottom[i,ID] = id;
					rowOneBottom[i,WIDTH] = width;
				}
				break;
				
			case 2:
				
				for(int i = bottomIndex; i < tempSizeBottom; i += 1){
					rowOneBottom[i,ID] = id;
					rowOneBottom[i,WIDTH] = width;
					rowTwoBottom[i,ID] = id;
					rowTwoBottom[i,WIDTH] = width;
				}
				break;
				
			case 3:
				
				for(int i = bottomIndex; i < tempSizeBottom; i += 1){
					rowOneBottom[i,ID] = id;
					rowOneBottom[i,WIDTH] = width;
					rowTwoBottom[i,ID] = id;
					rowTwoBottom[i,WIDTH] = width;
					rowThreeBottom[i,ID] = id;
					rowThreeBottom[i,WIDTH] = width;
				}
				break;
				
			case 4:
				
				for(int i = bottomIndex; i < tempSizeBottom; i += 1){
					rowOneBottom[i,ID] = id;
					rowOneBottom[i,WIDTH] = width;
					rowTwoBottom[i,ID] = id;
					rowTwoBottom[i,WIDTH] = width;
					rowThreeBottom[i,ID] = id;
					rowThreeBottom[i,WIDTH] = width;
					rowFourBottom[i,ID] = id;
					rowFourBottom[i,WIDTH] = width;
				}
				break;
		}
		
		bottomIndex = tempSizeBottom;

		//PrintBOTTOMState ();
	}

	public void RemoveBottomPiece(int id, int height, int width){

		/*
		print ("------------------------------------------------------------------");
		print ("REMOVE BOTTOM");
		print ("id: " + id);
		*/

		tempSizeBottomRemove = ARRAY - width;

		for (int i = 0; i < bottomIndex; i += 1) {
			if(bottomEdges[i] == id){

				//print ("id " + id + " FOUND: " + i);
				previousBottomIndex = i;
				break;
			}
		}

		for(int i = previousBottomIndex; i < tempSizeBottomRemove; i += 1){
			rowOneBottom[i,ID] = rowOneBottom[i + width,ID];
			rowOneBottom[i,WIDTH] = rowOneBottom[i + width,WIDTH];
			rowTwoBottom[i,ID] = rowTwoBottom[i + width,ID];
			rowTwoBottom[i,WIDTH] = rowTwoBottom[i + width,WIDTH];
			rowThreeBottom[i,ID] = rowThreeBottom[i + width,ID];
			rowThreeBottom[i,WIDTH] = rowThreeBottom[i + width,WIDTH];
			rowFourBottom[i,ID] = rowFourBottom[i + width,ID];
			rowFourBottom[i,WIDTH] = rowFourBottom[i + width,WIDTH];
		}

		bottomIndex -= width;

		PrintBOTTOMState ();
	}

	public void RemoveTopPiece(int id, int height, int width){
		/*
		print ("------------------------------------------------------------------");
		print ("REMOVE TOP");
		print ("id: " + id);
		*/
	
		tempSizeTopRemove = ARRAY - width;

		for (int i = 0; i < topIndex; i += 1) {
			if(topEdges[i] == id){

				//print ("id " + id + " FOUND: " + i);
				previousTopIndex = i;
				break;
			}
		}
		
		for(int i = previousTopIndex; i < tempSizeTopRemove; i += 1){
			rowOneTop[i,ID] = rowOneTop[i + width,ID];
			rowOneTop[i,WIDTH] = rowOneTop[i + width,WIDTH];
			rowTwoTop[i,ID] = rowTwoTop[i + width,ID];
			rowTwoTop[i,WIDTH] = rowTwoTop[i + width,WIDTH];
			rowThreeTop[i,ID] = rowThreeTop[i + width,ID];
			rowThreeTop[i,WIDTH] = rowThreeTop[i + width,WIDTH];
			rowFourTop[i,ID] = rowFourTop[i + width,ID];
			rowFourTop[i,WIDTH] = rowFourTop[i + width,WIDTH];
		}

		topIndex -= width;

		PrintTOPState ();
	}

	private void PrintTOPState(){
		top = ""; 

		print ("TOP");
		for(int i = 0; i < ARRAY; i += 1){
			top += System.Environment.NewLine + rowOneTop[i,ID] + "            " + rowTwoTop[i,ID] + "            " + rowThreeTop[i,ID] + "            " + rowFourTop[i,ID];

		}

		print (top);
		print ("------------------------------------------------------------------");

	}


	private void PrintBOTTOMState(){
		bottom = ""; 
		
		print ("BOTTOM");
		for(int i = 0; i < ARRAY; i += 1){
			bottom += System.Environment.NewLine + rowOneBottom[i,ID] + "            " + rowTwoBottom[i,ID] + "            " + rowThreeBottom[i,ID] + "            " + rowFourBottom[i,ID];
			
		}
		
		print (bottom);
		print ("------------------------------------------------------------------");
	}
}
