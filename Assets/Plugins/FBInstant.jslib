var FBPlugin = {
	

	SupportedAPI : function()
	{
		var returnStr = JSON.stringify(FBInstant.getSupportedAPIs());
    		var bufferSize = lengthBytesUTF8(returnStr) + 1;
    		var buffer = _malloc(bufferSize);
    		stringToUTF8(returnStr, buffer, bufferSize);
    		return buffer;
	},


	//Payments functionalities;
	
	PaymentIsReady : function()
	{
			FBInstant.payments.onReady(function () {
  			console.log('Payments Ready!');
			unityInstance.SendMessage("FBInstant_Initializer" , "OnPaymentReady_Callback");
			});	
		
		
	},
	
	
	PurchaseItem : function(id)
	{
		
			id =  Pointer_stringify(id);
		FBInstant.payments.purchaseAsync({
  			productID: id,
  			developerPayload: '',
			}).then(function (purchase) {
			var item = JSON.stringify(purchase);
  			//console.log(purchase);
			unityInstance.SendMessage("FBInstant_Initializer" , "purchaseItem_callback" , item);
			});
		
	},

	GetCatalog : function()
	{
		FBInstant.payments.getCatalogAsync().then(function (catalog) {
  			console.log(catalog); 
		});
	},


	ConsumePurchasedItem : function(id)
	{
		id = Pointer_stringify(id)
		FBInstant.payments.consumePurchaseAsync(id).then(function () {
			console.log("item consumed");
  			//unityInstance.SendMessage("FBInstant_Initializer" , "OnConsuming_callback");
		}).catch(function(error) {
			console.log("following error occured");
  			console.log(error);
			//unityInstance.SendMessage("FBInstant_Initializer" , "OnConsuming_callback" , JSON.stringify(error));
		});
	},

	GetPurchasedItem : function()
	{
		FBInstant.payments.getPurchasesAsync().then(function (purchases) {
  			
			var items = JSON.stringify(purchases);
			//console.log(items);
			unityInstance.SendMessage('FBInstant_Initializer' , 'purchasedProduct_callback' , items);
		});
	},

	//Leaderboard functionality

	GetContextID : function(leaderboardName)
	{
		leaderboardName = Pointer_stringify(leaderboardName);
		FBInstant.getLeaderboardAsync('contextual_leaderboard')
  		.then(function(leaderboard) {
    		console.log(leaderboard.getContextID()); // 12345678
  		});
		
	},

	GetLeaderboard : function(leaderboardName)
	{
		leaderboardName = Pointer_stringify(leaderboardName);

		console.log(leaderboardName);
		FBInstant.getLeaderboardAsync(leaderboardName + "." + FBInstant.context.getID())
  		.then(function(leaderboard) {
    			console.log(leaderboard.getName());
  		})
	},

	SetScoreOnLeaderboard : function(leaderboardName , score , extraData)
	{
		leaderboardName = Pointer_stringify(leaderboardName);
		extraData = Pointer_stringify(extraData);	
		FBInstant.getLeaderboardAsync(leaderboardName + "." + FBInstant.context.getID())
  		.then(function(leaderboard) {
    			return leaderboard.setScoreAsync(score, extraData);
  		})
  		.then(function(entry) {
    		console.log(entry.getScore());
    		console.log(entry.getExtraData());
  		});
	},

	
	
};

mergeInto(LibraryManager.library,FBPlugin);