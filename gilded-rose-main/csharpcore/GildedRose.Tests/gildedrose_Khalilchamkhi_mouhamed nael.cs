

	"package fr.xebia.katas.gildedrose;
	
public final class Item {
	
	    public int quality;
	    public int sellIn;
	    public final String name;
	
	    public Item(String name, int sellIn, int quality) {
	        this.quality = quality;
	        this.sellIn = sellIn;
	        this.name = name;
	    }
	
	}


"package fr.xebia.katas.gildedrose;

public final class Item {

    public int quality;
    public int sellIn;
    public final String name;

    public Item(String name, int sellIn, int quality) {
        this.quality = quality;
        this.sellIn = sellIn;
        this.name = name;
    }

}

package fr.xebia.katas.gildedrose;

public final class Item {

    public int quality;
    public int sellIn;
    public final String name;

    public Item(String name, int sellIn, int quality) {
        this.quality = quality;
        this.sellIn = sellIn;
        this.name = name;
    }

}

package fr.xebia.katas.gildedrose;

public final class Shop {

    Item[] items;

    public Shop(Item[] items) {
        this.items = items;
    }

    public void updateQuality() {
        for (int i = 0; i < items.length; i++) {
            if (!items[i].name.equals("Aged Brie")
                    && !items[i].name.equals("Backstage passes to a TAFKAL80ETC concert")) {
                if (items[i].quality > 0) {
                    if (!items[i].name.equals("Sulfuras, Hand of Ragnaros")) {
                        items[i].quality = items[i].quality - 1;
                    }
                }
            } else {
                if (items[i].quality < 50) {
                    items[i].quality = items[i].quality + 1;

                    if (items[i].name.equals("Backstage passes to a TAFKAL80ETC concert")) {
                        if (items[i].sellIn < 11) {
                            if (items[i].quality < 50) {
                                items[i].quality = items[i].quality + 1;
                            }
                        }

                        if (items[i].sellIn < 6) {
                            if (items[i].quality < 50) {
                                items[i].quality = items[i].quality + 1;
                            }
                        }
                    }
                }
            }

            if (!items[i].name.equals("Sulfuras, Hand of Ragnaros")) {
                items[i].sellIn = items[i].sellIn - 1;
            }

            if (items[i].sellIn < 0) {
                if (!items[i].name.equals("Aged Brie")) {
                    if (!items[i].name.equals("Backstage passes to a TAFKAL80ETC concert")) {
                        if (items[i].quality > 0) {
                            if (!items[i].name.equals("Sulfuras, Hand of Ragnaros")) {
                                items[i].quality = items[i].quality - 1;
                            }
                        }
                    } else {
                        items[i].quality = items[i].quality - items[i].quality;
                    }
                } else {
                    if (items[i].quality < 50) {
                        items[i].quality = items[i].quality + 1;
                    }
                }
            }
        }
    }
}

"import org.junit.Test;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.tuple;

public class ShopTest {

    @Test
    public void should_decrease_by_one_the_quality_and_remaining_sellIn_days_of_regular_items() {

        // Given
        final Item[] items = {
                new Item("+5 Dexterity Vest", 10, 20),
                new Item("Conjured Mana Cake", 3, 6)
        };

        // When
        new Shop(items).updateQuality();

        // Then
        assertThat(items)
                .extracting(item -> item.name, item -> item.sellIn, item -> item.quality)
                .containsExactly(
                        tuple("+5 Dexterity Vest", 9, 19),
                        tuple("Conjured Mana Cake", 2, 5)
                );
    }

    @Test
    public void should_increase_by_one_the_quality_of_products_that_get_better_as_they_age() {

        // Given
        final Item[] items = {
                new Item("Aged Brie", 2, 3),
                new Item("Backstage passes to a TAFKAL80ETC concert", 15, 20)
        };

        // When
        new Shop(items).updateQuality();

        // Then
        assertThat(items)
                .extracting(item -> item.name, item -> item.sellIn, item -> item.quality)
                .containsExactly(
                        tuple("Aged Brie", 1, 4),
                        tuple("Backstage passes to a TAFKAL80ETC concert", 14, 21)
                );
    }

    @Test
    public void should_increase_by_two_the_quality_of_products_that_get_better_as_they_age_when_there_are_10_days_or_less_left() {

        // Given
        final Item[] items = {new Item("Backstage passes to a TAFKAL80ETC concert", 8, 30)};

        // When
        new Shop(items).updateQuality();

        // Then
        assertThat(items)
                .extracting(item -> item.name, item -> item.sellIn, item -> item.quality)
                .containsExactly(tuple("Backstage passes to a TAFKAL80ETC concert", 7, 32));
    }

    @Test
    public void should_increase_by_three_the_quality_of_products_that_get_better_as_they_age_when_there_are_5_days_or_less_left() {

        // Given
        final Item[] items = {new Item("Backstage passes to a TAFKAL80ETC concert", 5, 15)};

        // When
        new Shop(items).updateQuality();

        // Then
        assertThat(items)
                .extracting(item -> item.name, item -> item.sellIn, item -> item.quality)
                .containsExactly(tuple("Backstage passes to a TAFKAL80ETC concert", 4, 18));
    }

    @Test
    public void should_decrease_quality_and_sellIn_of_products_twice_as_fast_when_we_have_passed_sellIn_date() {

        // Given
        final Item[] items = {
                new Item("+5 Dexterity Vest", 0, 20),
                new Item("Conjured Mana Cake", 0, 6)
        };

        // When
        new Shop(items).updateQuality();

        // Then
        assertThat(items)
                .extracting(item -> item.name, item -> item.sellIn, item -> item.quality)
                .containsExactly(
                        tuple("+5 Dexterity Vest", -1, 18),
                        tuple("Conjured Mana Cake", -1, 4)
                );
    }

    @Test
    public void should_update_quality_of_backstage_passes_to_zero_when_we_have_passed_the_sellIn_date() {

        // Given
        final Item[] items = {new Item("Backstage passes to a TAFKAL80ETC concert", 0, 20)};

        // When
        new Shop(items).updateQuality();

        // Then
        assertThat(items)
                .extracting(item -> item.name, item -> item.sellIn, item -> item.quality)
                .containsExactly(tuple("Backstage passes to a TAFKAL80ETC concert", -1, 0));
    }

    @Test
    public void should_not_alter_the_quality_of_Sulfuras() {

        // Given
        final Item[] items = {new Item("Sulfuras, Hand of Ragnaros", 10, 80)};

        // When
        new Shop(items).updateQuality();

        // Then
        assertThat(items)
                .extracting(item -> item.name, item -> item.sellIn, item -> item.qua

".DS_Store
.idea
node_modules
typings
app/**/*.js
app/**/*.js.map
test/**/*.js
test/**/*.js.map
coverage
.nyc_output"

"export class Item {
    name: string;
    sellIn: number;
    quality: number;

    constructor(name, sellIn, quality) {
        this.name = name;
        this.sellIn = sellIn;
        this.quality = quality;
    }
}

export class Shop {
    items: Array<Item>;

    constructor(items = []) {
        this.items = items;
    }

    updateQuality() {
        for (let i = 0; i < this.items.length; i++) {
            if (this.items[i].name != 'Aged Brie' && this.items[i].name != 'Backstage passes to a TAFKAL80ETC concert') {
                if (this.items[i].quality > 0) {
                    if (this.items[i].name != 'Sulfuras, Hand of Ragnaros') {
                        this.items[i].quality = this.items[i].quality - 1
                    }
                }
            } else {
                if (this.items[i].quality < 50) {
                    this.items[i].quality = this.items[i].quality + 1
                    if (this.items[i].name == 'Backstage passes to a TAFKAL80ETC concert') {
                        if (this.items[i].sellIn < 11) {
                            if (this.items[i].quality < 50) {
                                this.items[i].quality = this.items[i].quality + 1
                            }
                        }
                        if (this.items[i].sellIn < 6) {
                            if (this.items[i].quality < 50) {
                                this.items[i].quality = this.items[i].quality + 1
                            }
                        }
                    }
                }
            }
            if (this.items[i].name != 'Sulfuras, Hand of Ragnaros') {
                this.items[i].sellIn = this.items[i].sellIn - 1;
            }
            if (this.items[i].sellIn < 0) {
                if (this.items[i].name != 'Aged Brie') {
                    if (this.items[i].name != 'Backstage passes to a TAFKAL80ETC concert') {
                        if (this.items[i].quality > 0) {
                            if (this.items[i].name != 'Sulfuras, Hand of Ragnaros') {
                                this.items[i].quality = this.items[i].quality - 1
                            }
                        }
                    } else {
                        this.items[i].quality = this.items[i].quality - this.items[i].quality
                    }
                } else {
                    if (this.items[i].quality < 50) {
                        this.items[i].quality = this.items[i].quality + 1
                    }
                }
            }
        }

        return this.items;
    }
}"

"{
  "name": "typescript-mocha-kata-seed",
  "version": "1.4.0",
  "description": "Seed project for TDD code katas on TypeScript and mocha",
  "main": "index.js",
  "scripts": {
    "precompile": "rimraf app/**/*.js test/**/*.js",
    "compile": "tsc",
    "pretest": "rimraf app/**/*.js test/**/*.js",
    "test": "mocha --watch"
  },
  "author": "paucls",
  "homepage": "https://github.com/paucls/typescript-mocha-kata-seed",
  "license": "ISC",
  "private": true,
  "devDependencies": {
    "@types/chai": "~3.5.2",
    "@types/mocha": "~2.2.41",
    "@types/node": "~7.0.18",
    "chai": "~3.5.0",
    "mocha": "~3.2.0",
    "nyc": "~11.0.3",
    "rimraf": "~2.5.2",
    "ts-node": "~3.1.0",
    "typescript": "~2.2.0"
  },
  "nyc": {
    "extension": [
      ".ts"
    ],
    "exclude": [
      "**/*.d.ts",
      "test/**"
    ],
    "require": [
      "ts-node/register"
    ],
    "reporter": [
      "html",
      "text"
    ]
  }
}


"{
  "name": "typescript-mocha-kata-seed",
  "version": "1.4.0",
  "description": "Seed project for TDD code katas on TypeScript and mocha",
  "main": "index.js",
  "scripts": {
    "precompile": "rimraf app/**/*.js test/**/*.js",
    "compile": "tsc",
    "pretest": "rimraf app/**/*.js test/**/*.js",
    "test": "mocha --watch"
  },
  "author": "paucls",
  "homepage": "
  "private": true,
  "devDependencies": {
    "@types/chai": "~3.5.2",
    "@types/mocha": "~2.2.41",
    "@types/node": "~7.0.18",
    "chai": "~3.5.0",
    "mocha": "~3.2.0",
    "nyc": "~11.0.3",
    "rimraf": "~2.5.2",
    "ts-node": "~3.1.0",
    "typescript": "~2.2.0"
  },
  "nyc": {
    "extension": [
      ".ts"
    ],
    "exclude": [
      "**/*.d.ts",
      "test/**"
    ],
    "require": [
      "ts-node/register"
    ],
    "reporter": [
      "html",
      "text"
    ]
  }
}
"
--compilers ts-node/register
--require source-map-support/register
--recursive
--watch
test/**/*.ts

"{
    "compilerOptions": {
        "module": "commonjs",
        "target": "es5",
        "noImplicitAny": false,
        "sourceMap": false
    },
    "exclude": [
        "node_modules"
    ]"
