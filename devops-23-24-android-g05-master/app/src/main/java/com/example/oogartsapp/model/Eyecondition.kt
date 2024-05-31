package com.example.oogartsapp.model

/**
 * Represents an eyecondition.
 *
 * @property id The unique identifier for the eyecondition.
 * @property name The name of the eyecondition.
 * @property imageUrl The URL or path to the image associated with the eyecondition. Defaults to an empty string.
 * @property description The description or summary of the eyeeyecondition.
 * @property body Additional detailed information or content related to the eyecondition. Defaults to an empty string.
 * @property brochureUrl The URL or path to the brochure or additional resources for the eyecondition. Defaults to an empty string.
 */
data class Eyecondition(
    val id: Int,
    var name: String,
    var imageUrl: String? = "",
    var description: String,
    var body: String? = "",
    var brochureUrl: String? = "",
)
