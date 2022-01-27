# Unity Test Summary

This file contains a summary of the Unit Tests included in Ivan Boyko's Coding Challenge Submission, as well as the rationales behind their choice.
For this project, I chose unit tests that focused on the Llama class because it was the only class that had values that changed at runtime without player interaction.

## Assertion Summary

	CannotCaptureAPennedLlama()
	
This test will capture a llama, and then attempt to capture it again. Evalautes to true if the second capture attempt returns false.
This test was chosen to ensure the player cannot clip into captured llamas and recaputre them for easy coins.
	
	LlamaFeedingCannotExceedMaxHealth()
	
This test will capture a llama, and then immediately feed it. Evalautes to true if the llama's current health is no higher than its max health even after feeding.
This test was chosen to ensure the llamas cannote exceed their maximum health.
	
	LlamaHealthDecrementsWhileCaptured()
	
This test will capture a llama, store its current health value, and then wait a few seconds. Evaluates to true if the Llama's current health has gone down after the waiting period.
This test was chosen to ensure the llamas health correctly decays while penned.

## Extensions

If time permitted, more unit tests pertaining to llamas could be included such as checking for llama death on currenthealth reaching zero or confirming that llama health does not decrease while not penned.